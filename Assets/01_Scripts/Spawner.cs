using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> enemyPrefab;
    public float timeBtwSpawn = 0.5f;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        timer += Time.deltaTime;
        if (timer >= timeBtwSpawn)
        {
            timer = 0;
            //Instantiate(enemyPrefab,spawnPoints[Random.Range(0, spawnPoints.Count)].position,Quaternion.identity);
            int rSpawn = Random.Range(0, spawnPoints.Count);
            int rEnemy = Random.Range(0, enemyPrefab.Count);
            Instantiate(enemyPrefab[rEnemy], spawnPoints[rSpawn].position, spawnPoints[rSpawn].rotation);
        }
    }


}
