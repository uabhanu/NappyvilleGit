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
    Rigidbody2D m_foxBody2D;

    [Range(0.0f , 2.5f)] [SerializeField] float m_walkSpeed;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
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
    }

    FoxState GetState()
	{
		return m_currentState;
	}

    void Jump()
    {
        m_walkSpeed = 0f;
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
            break;

            case FoxState.JUMP:
                m_animator.SetBool("Jump" , true);
            break;

			case FoxState.WALK:
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
        m_walkSpeed = 0.4f;
        m_foxBody2D.velocity = new Vector2(-m_walkSpeed , m_foxBody2D.velocity.y);
    }
}
