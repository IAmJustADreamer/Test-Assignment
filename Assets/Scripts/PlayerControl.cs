using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerController playerController;
    private bool isFacingLeft = true;
    private float attackRange = 0.35f;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    private int takeDamage = 1;
    public int health = 10;
    

    public Animator playerAnimator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask powerUpLayers;
    public bool gameOver = false;


    private void Awake()
    {
        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    void Update()
    {
        //read left right inputs and call Attack and Rotate functions
        float move = playerController.Move.LeftRight.ReadValue<float>();

        //timer to prevent spamming
        if (Time.time >= nextAttackTime && !gameOver)
        {
            if (move < 0)
            {
                //check player input and rotation
                if (isFacingLeft)
                {
                    Attack();
                }
                else
                {
                    Rotate();
                    Attack();
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (move > 0)
            {
                if (!isFacingLeft)
                {
                    Attack();
                }
                else
                {
                    Rotate();
                    Attack();
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }      
    }

    private void Attack()
    {
        //play Attack animation
        playerAnimator.SetTrigger("Attack");

        //check if enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(takeDamage);
        }

        //pick up power ups
        Collider2D[] hitPowerUp = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, powerUpLayers);
        foreach (Collider2D powerUp in hitPowerUp)
        {
            powerUp.GetComponent<HealthPowerUp>().HealPlayer();
        }
    }

    //rotate player
    private void Rotate()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        //take damage
        health -= damage;
        GameObject.Find("Canvas").GetComponent<UIUpdate>().UpdateHealth(health);

        if (health <= 0)
        {
            //play death animations
            Die();
        }
    }

    private void Die()
    {
        gameOver = true;
        playerAnimator.SetTrigger("Death");
        GameObject.Find("Canvas").GetComponent<UIUpdate>().DeathScreen();
    }
}
