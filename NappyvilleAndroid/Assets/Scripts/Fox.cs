using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Animator m_animator;
    bool m_isWalking;
    float m_walkSpeed;

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

        WalkAnimation();
	}
	
	void AttackAnimation()
    {
        m_walkSpeed = 0f;
        //m_isWalking = false;
        m_animator.SetBool("Attack" , true);
        m_animator.SetBool("Jump" , false);
    }

    void JumpAnimation()
    {
        m_walkSpeed = 0f;
        //m_isWalking = false;
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Jump" , true);
    }

    //public void SetSpeed(float speed)
    //{
    //    m_walkSpeed = speed;
    //}

    void WalkAnimation()
    {
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Jump" , false);
        m_animator.SetBool("Walk" , true);
        //m_isWalking = true;
        m_walkSpeed = 0.4f;
        transform.Translate(Vector2.left * m_walkSpeed * Time.deltaTime);
    }
}
