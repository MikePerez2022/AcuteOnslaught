using System.Collections.Generic;
using UnityEngine;
public class WaveSystem : MonoBehaviour
{
    private float timeAlive = 0.0f;
    private int waveDifficulty = 1;
    private float bossInterval = 0;
    private float waveDuration = 1;
    public float spawnTimer;
    public List<GameObject> enemyTypes = new List<GameObject>();
    public List<GameObject> bossTypes = new List<GameObject>();
    public List<Transform> spawnpoints = new List<Transform>();
    private List<GameObject> currentWave = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateWave();
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
                CreateWave();
            }
            
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

        // Spawn boss and increment wave difficulty
        if (bossInterval >= 40)
        {
            Instantiate(bossTypes[Random.Range(0, bossTypes.Count)], spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
            waveDifficulty++;
            bossInterval = 0;
        }
    }
    void CreateWave()
    {
        // Notify Wave Started
        currentWave = SelectWaveEnemies();
        spawnTimer = waveDuration / currentWave.Count;
    }
    List<GameObject> SelectWaveEnemies()
    {
        foreach (GameObject enemy in enemyTypes)
        {
            currentWave.Add(enemy);
        }
        return currentWave;
    }
}