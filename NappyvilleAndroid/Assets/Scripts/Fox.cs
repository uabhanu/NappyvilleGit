using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Animator m_animator;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
	}
	
	void Attack()
    {
        m_animator.SetBool("Attack" , true);
        m_animator.SetBool("Jump" , false);
    }

    void Jump()
    {
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Jump" , true);
    }

    void Walk()
    {
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Jump" , false);
    }
}
