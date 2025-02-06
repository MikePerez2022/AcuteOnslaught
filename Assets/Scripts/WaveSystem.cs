using System.Collections.Generic;
using UnityEngine;
public class WaveSystem : MonoBehaviour
{
    private float timeAlive = 0.0f;
    private int waveDifficulty = 1;
    private float bossInterval = 0;
    public List<GameObject> enemyTypes = new List<GameObject>();
    public List<GameObject> bossTypes = new List<GameObject>();
    public List<Transform> spawnpoints = new List<Transform>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        bossInterval += Time.deltaTime;
    }
    void CreateWave()
    {
        // Notify Wave Started
        List<GameObject> currentWave = SelectWaveEnemies();
        
        // Spawn Enemies
        foreach (GameObject enemy in currentWave)
        {
            Instantiate(enemy, spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
        }

        if (bossInterval >= 40)
        {
            Instantiate(bossTypes[Random.Range(0, bossTypes.Count)], spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
            bossInterval = 0;
            waveDifficulty++;
        }
    }
    List<GameObject> SelectWaveEnemies()
    {
        List<GameObject> currentWave = new List<GameObject>();
        foreach (GameObject enemy in enemyTypes)
        {
            currentWave.Add(enemy);
        }
        return currentWave;
    }
}