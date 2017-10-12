using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrophy : MonoBehaviour
{
    Animator m_animator;

    [HideInInspector] [Range(0.0f , 10.0f)] [SerializeField] float m_lineDistance , m_lineStartingPoint;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
        StartCoroutine("AnimsRoutine");
	}
	
	IEnumerator AnimsRoutine()
    {
        yield return new WaitForSeconds(4f);
        m_animator.SetBool("Star" , true);

        yield return new WaitForSeconds(2.5f);
        m_animator.SetBool("Star" , false);

        StartCoroutine("AnimsRoutine");
    }
}
