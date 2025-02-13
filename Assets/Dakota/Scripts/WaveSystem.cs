using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSystem : MonoBehaviour
{
    private float timeAlive = 0f;
    private float bossTimer = 0f;
    private float waveInterval = 5f; // Time between waves
    private float spawnTimer = 0f;
    private float spawnInterval = 0f;
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
        bossTimer += Time.deltaTime;
        if (spawnTimer <= 0)
        {
            // Spawn Enemies
            if (currentWave.Count > 0)
            {
                int randIndex = Random.Range(0, currentWave.Count);
                Instantiate(currentWave[randIndex], spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
                currentWave.RemoveAt(randIndex);
                spawnTimer = spawnInterval;
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
        if (bossTimer >= 40)
        {
            // Fully random, better structure can be implemented once we have more details on wave progression
            Instantiate(bossTypes[Random.Range(0, bossTypes.Count)], spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
            bossTimer = 0;
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
        float waveDifficulty = (timeAlive <= 30) ? 1 : timeAlive / 30;
        int waveCost = (int)(waveDifficulty * 10);
        while (waveCost > 0)
        {
            int randIndex = Random.Range(0, enemyTypes.Count);
            
            int randEnemyCost = enemyTypes[randIndex].GetComponent<AIScoring>().cost;
            // Need to make sure it can always find an enemy to add to the wave, always have one enemy with a cost of 1 for example
            if (waveCost - randEnemyCost >= 0)
            {
                currentWave.Add(enemyTypes[randIndex]);
                waveCost -= randEnemyCost;
            }
            
            if (waveCost == 0)
            {
                break;
            }
        }
        spawnInterval = waveInterval / (float)currentWave.Count;
        spawnTimer = spawnInterval;
    }
}