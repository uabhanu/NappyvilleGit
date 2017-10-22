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

    public int m_enemyCount = 0;

	void Start()
    {
        m_levelManager = FindObjectOfType<LevelManager>();

        //StartCoroutine("RearrangeRoutine");

        if(m_levelManager.m_totalEnemiesKilled < m_levelManager.m_enemyKillTarget)
        {
            StartCoroutine("SpawnRoutine");
        }
	}

    IEnumerator RearrangeRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        if(transform.childCount == 0)
        {
            if(m_levelManager.m_currentSceneIndex == 3)
            {
                transform.position = new Vector2(transform.position.x , Random.Range(1 , 4));
            }

            if(m_levelManager.m_currentSceneIndex > 3)
            {
                transform.position = new Vector2(transform.position.x , Random.Range(0 , 5));
            }
        }
        
        StartCoroutine("RearrangeRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_timeToSpawn);

        if(m_enemyCount < m_maxEnemies && m_levelManager.m_totalEnemiesKilled < m_levelManager.m_enemyKillTarget)
        {
            m_enemyObj = Instantiate(m_enemyPrefabs[Random.Range(0 , m_enemyPrefabs.Length)]) as GameObject;
            m_enemyObj.transform.parent = transform;
            m_enemyObj.transform.position = transform.position;
            m_enemyCount++;
        }

        if(m_levelManager.m_gameTime >= 15f)
        {
            if(m_levelManager.m_currentSceneIndex == 2)
            {
                m_maxEnemies = 1;
            }

            if(m_levelManager.m_gameTime >= 45f)
            {
                m_maxEnemies = 1;
            }

            if(m_levelManager.m_gameTime >= 60f)
            {
                m_maxEnemies = 2;
            }

            if(m_levelManager.m_gameTime >= 100f)
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
