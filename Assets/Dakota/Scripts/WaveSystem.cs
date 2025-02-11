using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSystem : MonoBehaviour
{
    private float timeAlive = 0f;
    private float waveDifficulty;
    private float bossInterval = 0f;
    private int waveInterval = 10; // Time between waves
    private float spawnTimer;
    private List<GameObject> currentWave = new List<GameObject>();
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();
    [SerializeField] List<GameObject> bossTypes = new List<GameObject>();
    [SerializeField] List<Transform> spawnpoints = new List<Transform>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectWaveEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        bossInterval += Time.deltaTime;
        if (spawnTimer <= 0)
        {
            // Spawn Enemies
            if (currentWave.Count > 0)
            {
                int randIndex = Random.Range(0, currentWave.Count);
                Instantiate(currentWave[randIndex], spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
                currentWave.RemoveAt(randIndex);
            }
            else
            {
                SelectWaveEnemies();
            }
            
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

        // Spawn boss and increment wave difficulty
        if (bossInterval >= 40)
        {
            // Fully random, better structure can be implemented once we have more details on wave progression
            Instantiate(bossTypes[Random.Range(0, bossTypes.Count)], spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
            waveDifficulty++;
            bossInterval = 0;
        }

        if (timeAlive % 60 == 0)
        {
            // Increase score every minute (Adjustable), increased bonus points based on time
            // ScoreManager.ChangeScore(100, (int)(timeAlive / 60)); 
        }
    }
    void SelectWaveEnemies()
    {
        // Adjust logic to select enemies based on wave difficulty to what we want
        waveDifficulty = Math.Min(1, timeAlive / 60);
        int waveCost = (int)(waveDifficulty * 10);
        while (waveCost > 0)
        {
            int randIndex = Random.Range(0, enemyTypes.Count);
            // Need enemies to have cost variable
            /*
            int randEnemyCost = enemyTypes[randIndex].GetComponent<Enemy>().cost;
            // Need to make sure it can always find an enemy to add to the wave, always have one enemy with a cost of 1 for example
            if (waveCost - randEnemyCost >= 0)
            {
                currentWave.Add(enemyTypes[randIndex]);
                waveCost -= randEnemyCost;
            }
            else if (waveCost <= 0)
            {
                break;
            }
            */
        }
        spawnTimer = waveInterval / currentWave.Count;
    }
}