using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private bool dead = false;
    private float distanceToPlayer = 0.9f;
    private float attackRange = 0.2f;
    private float attackRate = 1.2f;
    private float nextAttackTime = 0f;
    private float speed;
    private int health = 1;
    private int takeDamage = 1;

    public Transform attackPoint;
    public LayerMask playerLayer;
    public Animator enemyAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        //generate random speed for each enemy
        speed = Random.Range(3.0f, 10.0f);
    }

    void FixedUpdate()
    {
        //check if enemy have died
        if (!dead)
        {
            //make enemies move towards player and attack him
            Vector3 direction = player.transform.position - transform.position;
            if (Mathf.Abs(direction.x) > distanceToPlayer)
            {
                direction.Normalize();
                rb.MovePosition((transform.position + (direction * speed) * Time.deltaTime) );
            }
            else if (Mathf.Abs(direction.x) < distanceToPlayer && Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        //take damage
        health -= damage;

        dead = true;

        //play damage animation and death animations
        Die();
    }

    //play death animation and delete enemy
    void Die()
    {
        enemyAnimator.SetTrigger("Hit");
        enemyAnimator.SetTrigger("Dead");
        GameObject.Find("Canvas").GetComponent<UIUpdate>().UpdateDeathCounter(1);
        Destroy(this.gameObject, 0.3f);
    }

    private void Attack()
    {
        nextAttackTime = Time.time + 2f / attackRate;

        //play Attack animation
        enemyAnimator.SetTrigger("Attack");
        StartCoroutine(HitDelay());
    }

    private IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(1.5f);
        if (!dead)
        {
            //check if enemies in range
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            //damage enemies
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<PlayerControl>().TakeDamage(takeDamage);
            }
        }
    }
}
