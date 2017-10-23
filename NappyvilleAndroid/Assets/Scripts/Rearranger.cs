using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rearranger : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemySpawnerObjs;

    [SerializeField] int m_i;

    [SerializeField] Vector2[] m_rearrangePositions;

	void Start()
    {
        StartCoroutine("RearrangeRoutine");
	}
	
	IEnumerator RearrangeRoutine()
    {
        m_i = Random.Range(0 , 3);

        yield return new WaitForSeconds(2f);

        m_enemySpawnerObjs = GameObject.FindGameObjectsWithTag("EnemySpawner");

        for(m_i = 0; m_i < m_enemySpawnerObjs.Length; m_i++)
        {
            m_enemySpawnerObjs[m_i].transform.position = m_rearrangePositions[Random.Range(m_i , m_i + 1)];
        }
        
        StartCoroutine("RearrangeRoutine");
    }
}
