using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] GameObject m_enemyAttacking;

    [Range(20 , 400)] public int m_hitpoints;

	void Start()
    {
		StartCoroutine("DieRoutine");
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
}
