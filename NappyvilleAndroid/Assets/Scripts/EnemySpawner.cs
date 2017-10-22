using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int m_maxEnemies = 0;
    LevelManager m_levelManager;

    [SerializeField] float m_timeToSpawn;

    [SerializeField] GameObject m_enemyObj;

    [SerializeField] GameObject[] m_enemyPrefabs;

    public bool m_spawn;
    public int m_enemyCount = 0;

	void Start()
    {
        m_levelManager = FindObjectOfType<LevelManager>();

        if(m_levelManager.m_totalEnemiesKilled < m_levelManager.m_enemyKillTarget)
        {
            StartCoroutine("SpawnRoutine");
        }
	}

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_timeToSpawn);

        if(m_enemyCount < m_maxEnemies && m_levelManager.m_totalEnemiesKilled < m_levelManager.m_enemyKillTarget && m_spawn)
        {
            m_enemyObj = Instantiate(m_enemyPrefabs[Random.Range(0 , m_enemyPrefabs.Length)]) as GameObject;
            m_enemyObj.transform.parent = transform;
            m_enemyObj.transform.position = transform.position;
            m_enemyCount++;
        }

        if(m_levelManager.m_gameTime >= 15f)
        {
            m_maxEnemies = 1;

            if(m_levelManager.m_gameTime >= 60f)
            {
                m_maxEnemies = 2;
            }

            if(m_levelManager.m_gameTime >= 120f)
            {
                m_maxEnemies = 3;
            }

            if(m_levelManager.m_gameTime >= 240f)
            {
                m_maxEnemies = 5;
            }
        }

        StartCoroutine("SpawnRoutine");
    }
}
