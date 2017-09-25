using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public enum FoxState
	{
		ATTACK,
		JUMP,
		WALK,
	};
	
	public FoxState m_currentState;
	public FoxState m_previousState;

    Animator m_animator;
    float m_currentWalkSpeed;
    Rigidbody2D m_foxBody2D;

    [Range(0.0f , 2.5f)] [SerializeField] float m_walkSpeed;

    [SerializeField] GameObject m_currentTarget;

    [Range(20 , 100)] [SerializeField] int m_attack;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
        m_currentWalkSpeed = m_walkSpeed;
        m_foxBody2D = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        UpdateAnimations();
        UpdateStateMachine();
	}

	void Attack()
    {
        m_walkSpeed = 0f;
        m_foxBody2D.velocity = new Vector2(-m_walkSpeed , m_foxBody2D.velocity.y);
    }

    void BackToWalk()
    {
        SetState(FoxState.WALK);
    }

    void CauseDamage()
    {
        m_currentTarget.gameObject.GetComponent<DefendersHitpoints>().m_hitpoints -= m_attack;
    }

    FoxState GetState()
	{
		return m_currentState;
	}

    void Jump()
    {
        m_walkSpeed = 3f;
        m_foxBody2D.velocity = new Vector2(-m_walkSpeed , m_foxBody2D.velocity.y);   
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Fox Collision with Player Successful"); //Working Fine
            m_currentTarget = tri2D.gameObject;
            SetState(FoxState.ATTACK);
        }
        
        if(tri2D.gameObject.tag.Equals("Gstone"))
        {
            //Debug.Log("Fox Collision with Gravestone Successful"); //Working Fine
            SetState(FoxState.JUMP);
        }
    }

    public void SetState(FoxState newState)
	{
		if (m_currentState == newState)
		{
			return;
		}
		
		m_previousState = m_currentState;
		m_currentState = newState;
	}

    void UpdateAnimations()
	{
		switch(m_currentState)
		{
			case FoxState.ATTACK:
                m_animator.SetBool("Attack" , true);
                m_animator.SetBool("Jump" , false);
                m_animator.SetBool("Walk" , false);
            break;

            case FoxState.JUMP:
                m_animator.SetBool("Attack" , false);
                m_animator.SetBool("Jump" , true);
                m_animator.SetBool("Walk" , false);
            break;

			case FoxState.WALK:
                m_animator.SetBool("Attack" , false);
                m_animator.SetBool("Jump" , false);
                m_animator.SetBool("Walk" , true);
            break;
		}
	}

    void UpdateStateMachine()
	{
		switch(m_currentState)
		{
			case FoxState.ATTACK:
				Attack();
			break;

			case FoxState.JUMP:
				Jump();
			break;
			
			case FoxState.WALK: 
				Walk();
			break;
		}
	}

    void Walk()
    {
        m_walkSpeed = m_currentWalkSpeed;
        m_foxBody2D.velocity = new Vector2(-m_walkSpeed , m_foxBody2D.velocity.y);
    }
}
