using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravestone : MonoBehaviour
{
    Animator m_animator;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
	}
	
	void Attacked()
    {
        m_animator.SetBool("Attacked" , true);
    }

    void BackToIdle()
    {
        m_animator.SetBool("Attacked" , false);
    }
}
