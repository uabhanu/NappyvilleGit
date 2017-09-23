using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    Animator m_animator;
    bool m_isWalking;

    [Range(0.0f , 2.5f)] [SerializeField] float m_walkSpeed;

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
	
	void AttackAnimation()
    {
        //m_isWalking = false;
        m_animator.SetBool("Attack" , true);
        m_animator.SetBool("Jump" , false);
    }

    public void SetSpeed(float speed)
    {
        m_walkSpeed = speed;
    }

    void WalkAnimation()
    {
        m_animator.SetBool("Attack" , false);
        m_animator.SetBool("Jump" , false);
        m_animator.SetBool("Walk" , true);
        //m_isWalking = true;
    }
}
