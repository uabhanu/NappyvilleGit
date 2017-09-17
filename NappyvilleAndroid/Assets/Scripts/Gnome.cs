using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    Animator m_animator;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
        StartCoroutine("AnimsRoutine");
	}
	
	IEnumerator AnimsRoutine()
    {
        yield return new WaitForSeconds(3f);
        m_animator.SetBool("Hop" , true);

        yield return new WaitForSeconds(3f);
        m_animator.SetBool("Hop" , false);

        StartCoroutine("AnimsRoutine");
    }
}
