using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    Animator m_animator;
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

        transform.Translate(Vector2.left * m_walkSpeed * Time.deltaTime);
	}

    public void SetSpeed(float speed)
    {
        m_walkSpeed = speed;
    }

    void AttackAnimation()
    {
        m_animator.SetBool("Attack" , true);
        m_animator.SetBool("Walk" , false);
    }

    void WalkAnimation()
    {
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Walk" , true);   
    }
}
