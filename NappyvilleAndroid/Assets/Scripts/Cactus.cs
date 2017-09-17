using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
	Animator m_animator;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
        StartCoroutine("AnimsRoutine");
	}
	
	IEnumerator AnimsRoutine()
    {
        yield return new WaitForSeconds(4f);
        m_animator.SetBool("Attack" , true);

        yield return new WaitForSeconds(3f);
        m_animator.SetBool("Attack" , false);

        StartCoroutine("AnimsRoutine");
    }
}
