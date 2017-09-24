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

    IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.9f);
        SetState(FoxState.WALK);
    }
	
	void Attack()
    {
        m_walkSpeed = 0f;
        m_foxBody2D.velocity = new Vector2(-m_walkSpeed , m_foxBody2D.velocity.y);
    }

    FoxState GetState()
	{
		return m_currentState;
	}

    void Jump()
    {
        m_walkSpeed = 0f;
        m_foxBody2D.velocity = new Vector2(-m_walkSpeed , m_foxBody2D.velocity.y);
        StartCoroutine("JumpRoutine");
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            SetState(FoxState.ATTACK);
        }

        if(tri2D.gameObject.tag.Equals("Gstone"))
        {
            SetState(FoxState.JUMP);
        }

        else
        {
            Debug.LogError("Collision with Player Failed");
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
