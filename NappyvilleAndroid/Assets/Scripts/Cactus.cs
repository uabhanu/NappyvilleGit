using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public enum CactusState
	{
        ATTACK,
		IDLE,
	};
	
	public CactusState m_currentState;
	public CactusState m_previousState;

	Animator m_animator;

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

        UpdateAnimations();
        UpdateStateMachine();
	}

    void Attack()
    {

    }
	
    CactusState GetState()
	{
		return m_currentState;
	}

    public void SetState(CactusState newState)
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
            case CactusState.ATTACK:
                m_animator.SetBool("Attack" , true);
            break;

			case CactusState.IDLE:
                m_animator.SetBool("Attack" , false);
            break;
		}
	}

    void UpdateStateMachine()
	{
		switch(m_currentState)
		{
			case CactusState.ATTACK:
				Attack();
			break;
			
			case CactusState.IDLE: 
				
			break;
		}
	}
}
