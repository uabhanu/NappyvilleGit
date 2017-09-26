using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackersHitpoints : MonoBehaviour
{
    [Range(100 , 400)] public int m_hitpoints;

	void Start()
    {
		StartCoroutine("DieRoutine");
	}

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(1f);

        if(m_hitpoints == 0)
        {
            Destroy(gameObject); //This is ok for now but create a method to destroy and call that in animation event after calling SetState(Fox or Lizard Walk) somehow
        }

        StartCoroutine("DieRoutine");
    }
}
