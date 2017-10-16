using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemyPrefabs;

	void Start()
    {
		
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        foreach(GameObject thisEnemy in m_enemyPrefabs)
        {
            if(TimeToSpawn(thisEnemy))
            {
                Spawn(thisEnemy);
            }
        }
	}

    bool TimeToSpawn(GameObject enemyObj)
    {
        BhanuEnemy bhanuEnemy = enemyObj.GetComponent<BhanuEnemy>();
        float meanSpawnDelay = bhanuEnemy.m_seenEverySecs;
        float spawnsPerSec = 1 / meanSpawnDelay;

        if(Time.deltaTime > meanSpawnDelay)
        {
            Debug.LogError("Sir Bhanu, Spawn rate capped by frame rate");
        }
        
        float threshold = spawnsPerSec * Time.deltaTime / 10;

        if(Random.value < threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Spawn(GameObject enemyObjToSpawn)
    {
        GameObject enemy = Instantiate(enemyObjToSpawn) as GameObject;
        enemy.transform.parent = transform;
        enemy.transform.position = transform.position;
    }
}
