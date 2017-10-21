using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int m_maxEnemies = 1;

    [SerializeField] float m_timeToSpawn;

    [SerializeField] GameObject m_enemyObj , m_enemyPrefab;

    public int m_enemyCount = 0;

	void Start()
    {
		StartCoroutine("SpawnRoutine");
	}

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_timeToSpawn);

        if(m_enemyCount < m_maxEnemies)
        {
            m_enemyObj = Instantiate(m_enemyPrefab) as GameObject;
            m_enemyObj.transform.parent = transform;
            m_enemyObj.transform.position = transform.position;
            m_enemyCount++;
        }

        if(SurvivalProgress.m_gameTimeSlider.value >= 0.5f && m_maxEnemies < 2)
        {
            m_maxEnemies++;
            m_timeToSpawn -= (m_timeToSpawn - 2);
        }
        
        StartCoroutine("SpawnRoutine");
    }
}
