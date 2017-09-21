using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Animator m_animator;
    bool m_isWalking;

    [Range(0.0f , 1.5f)] [SerializeField] float m_walkSpeed;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
	}

    void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        Walk();
	}
	
	void Attack()
    {
        m_isWalking = false;
        m_animator.SetBool("Attack" , true);
        m_animator.SetBool("Jump" , false);
    }

    void Jump()
    {
        m_isWalking = false;
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Jump" , true);
    }

    void Walk()
    {
        m_isWalking = true;
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Jump" , false);
        m_animator.SetBool("Walk" , true);

        if(m_isWalking)
        {
            transform.Translate(Vector2.left * m_walkSpeed * Time.deltaTime);
        }
    }
}
