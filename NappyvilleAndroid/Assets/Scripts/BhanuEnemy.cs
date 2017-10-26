using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuEnemy : MonoBehaviour
{
    LevelManager m_levelManager;

    public float m_seenEverySecs;
    [Range(20 , 400)] public int m_hitpoints;

	void Start()
    {
        m_levelManager = FindObjectOfType<LevelManager>();
        StartCoroutine("DieRoutine");
	}

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        if(m_hitpoints <= 0)
        {
            if(EnemySpawner.m_enemyCount > 0)
            {
                EnemySpawner.m_enemyCount--;
            }
            
            m_levelManager.m_totalEnemiesKilled++;
            Destroy(gameObject);
        }

        if(transform.position.x < 0.4f)
        {
            m_levelManager.LoadScene("07Lose");
        }

        StartCoroutine("DieRoutine");
    }

    void OnMouseDown()
    {
        LevelManager.Disable(LevelManager.m_cantAffordMessage);
    }
}
