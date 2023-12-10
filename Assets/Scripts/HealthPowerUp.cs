using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        Destroy(this.gameObject, 3.0f); 
    }

    public void HealPlayer()
    {
        player.GetComponent<PlayerControl>().TakeDamage(-1);
        Destroy(this.gameObject);
    }
}
