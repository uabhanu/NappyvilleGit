using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuPlayer : MonoBehaviour
{
    Animator m_animator;

    EnemySpawner m_myLaneSpawner;

    public bool m_enemyDetected;

    [Range(20 , 400)] public int m_hitpoints;

	void Start()
    {
		m_animator = GetComponent<Animator>();
        StartCoroutine("DieRoutine");

        LaneSpawInfo();
        print(m_myLaneSpawner);
	}

    void FixedUpdate()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(EnemyDetected())
        {
            m_enemyDetected = true;
        }
        else
        {
            m_enemyDetected = false;
        }
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(1f);

        if(m_hitpoints <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine("DieRoutine");
    }

    bool EnemyDetected()
    {
        if(m_myLaneSpawner.transform.childCount <= 0)
        {
            m_enemyDetected = false;
            return m_enemyDetected;
        }

        foreach(Transform bhanuEnemy in m_myLaneSpawner.transform) // Unity Docs indicates that Transforms also support enumeratiors so we can actually loop through children
        {
            if(bhanuEnemy.transform.position.x > transform.position.x) //Also check if bhanuEnemy is visible using && later
            {
                m_enemyDetected = true;
                return m_enemyDetected;
            }
        }

        m_enemyDetected = false;
        return m_enemyDetected;
    }

    void LaneSpawInfo()
    {
        EnemySpawner[] m_enemySpawners = FindObjectsOfType<EnemySpawner>();

        foreach(EnemySpawner m_enemySpawner in m_enemySpawners)
        {
            if(m_enemySpawner.transform.position.y == transform.position.y)
            {
                m_myLaneSpawner = m_enemySpawner;
                return;
            }
            else
            {
                Debug.LogError(name + ": Dear Sir, can't find the spawner in lane");
            }
        }
    }
}
