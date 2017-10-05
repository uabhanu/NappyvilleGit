﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public enum GnomeState
	{
        ATTACK,
		IDLE,
	};
	
	public GnomeState m_currentState;
	public GnomeState m_previousState;

	Animator m_animator;
    GameObject m_projectilesParent;

    [SerializeField] GameObject m_axePrefab;

    [SerializeField] Transform m_spawnPosition;

	void Start()
    {
	    m_animator = GetComponent<Animator>();

        m_projectilesParent = GameObject.Find("Projectiles");

        if(m_projectilesParent == null)
        {
            m_projectilesParent = new GameObject("Projectiles");
        }
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
        GameObject axe = Instantiate(m_axePrefab , m_spawnPosition) as GameObject;

        if(m_projectilesParent != null)
        {
            axe.transform.parent = m_projectilesParent.transform;
        }
    }

    GnomeState GetState()
	{
		return m_currentState;
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
            case GnomeState.ATTACK:
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
			case GnomeState.ATTACK:
				
			break;
			
			case GnomeState.IDLE: 
				
			break;
		}
	}
}
