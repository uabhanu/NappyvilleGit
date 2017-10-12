using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuPlayer : MonoBehaviour
{
    [Range(20 , 400)] public int m_hitpoints;

	void Start()
    {
		StartCoroutine("DieRoutine");
	}

    void FixedUpdate()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        EnemyDetected();
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
        //RaycastHit method is failure so do async per Udemy Tutorial, you may have to go back a lecture
        return true;
    }
}
