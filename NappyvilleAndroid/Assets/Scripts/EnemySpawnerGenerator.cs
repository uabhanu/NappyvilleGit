using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemySpawnerPrefabs;

    [SerializeField] Vector2[] m_spawnLocations;

	void Start()
    {
		for(int i = 0; i < m_enemySpawnerPrefabs.Length; i++)
        {
            GameObject enemySpawner = Instantiate(m_enemySpawnerPrefabs[i] , m_spawnLocations[i] , transform.rotation) as GameObject;
            enemySpawner.transform.parent = transform;
            enemySpawner.transform.position = m_spawnLocations[i];
        }
	}
	
	void Update()
    {
		
	}
}
