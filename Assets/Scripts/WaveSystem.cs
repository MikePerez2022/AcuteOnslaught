using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private float timeAlive = 0.0f;
    private float waveDifficulty = 1f;
    public List<GameObject> enemySpawnList = new List<GameObject>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
    }

    void CreateWave()
    {
        
    }

    void SelectWaveEnemies()
    {
        
    }
}
