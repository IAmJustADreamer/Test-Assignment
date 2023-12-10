using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int spawnNumber;
    private float delaySpawnEnemy = 2.0f;
    private float delayPowerUpSpawn = 10.0f;

    public GameObject enemy;
    public GameObject powerUp;
    public Transform[] enemiesSpawnPoints;
    public Transform[] powerUpSpawnPoints;

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerControl>().gameOver == false)
        {
            if (Time.time >= delaySpawnEnemy)
            {
                ChooseSpawn();
                delaySpawnEnemy = Time.time + Random.Range(1f, 3f);
                Instantiate(enemy, enemiesSpawnPoints[spawnNumber].position, enemiesSpawnPoints[spawnNumber].rotation);
            }
            else if (Time.time >= delayPowerUpSpawn)
            {
                ChooseSpawn();
                delayPowerUpSpawn = Time.time + Random.Range(10.0f, 15.0f);
                Instantiate(powerUp, powerUpSpawnPoints[spawnNumber].position, powerUpSpawnPoints[spawnNumber].rotation);
            }
        }
    }

    private void ChooseSpawn()
    {
        spawnNumber = Random.Range(0, 2);
    }
}
