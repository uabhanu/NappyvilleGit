using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int m_children = 0;
    LevelManager m_levelManager;

    [SerializeField] float m_timeToSpawn;

    [SerializeField] GameObject m_enemyObj;

    [SerializeField] GameObject[] m_enemyPrefabs;

	void Start()
    {
        m_levelManager = FindObjectOfType<LevelManager>();
        StartCoroutine("SpawnRoutine");
	}

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_timeToSpawn);

        if(m_levelManager.m_totalEnemiesKilled < m_levelManager.m_enemyKillTarget && transform.childCount < m_children)
        {
            m_enemyObj = Instantiate(m_enemyPrefabs[Random.Range(0 , m_enemyPrefabs.Length)]) as GameObject;
            m_enemyObj.transform.parent = transform;
            m_enemyObj.transform.position = transform.position;
        }

        if(m_levelManager.m_gameTime >= 12.5f)
        {
            if(m_levelManager.m_currentSceneIndex == 2)
            {
                m_children = 1;

                if(m_levelManager.m_gameTime >= 30f)
                {
                    m_children = 2;
                }
            }

            if(m_levelManager.m_gameTime >= 25f)
            {
                m_children = 1;
            }

            if(m_levelManager.m_gameTime >= 40f)
            {
                m_children = 2;
            }

            if(m_levelManager.m_gameTime >= 60f)
            {
                m_children = 3;
            }
        }

        StartCoroutine("SpawnRoutine");
    }
}
