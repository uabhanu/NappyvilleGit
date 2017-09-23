using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public enum GnomeState
	{
        HOP,
		IDLE,
	};
	
	public GnomeState m_currentState;
	public GnomeState m_previousState;

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

    GnomeState GetState()
	{
		return m_currentState;
	}

    void Hop()
    {

    }

    public void SetState(GnomeState newState)
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
            case GnomeState.HOP:
                m_animator.SetBool("Hop" , true);
            break;

			case GnomeState.IDLE:
                m_animator.SetBool("Hop" , false);
            break;
		}
	}

    void UpdateStateMachine()
	{
		switch(m_currentState)
		{
			case GnomeState.HOP:
				Hop();
			break;
			
			case GnomeState.IDLE: 
				
			break;
		}
	}
}
