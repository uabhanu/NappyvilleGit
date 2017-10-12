﻿using System.Collections;
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
    GameObject m_projectilesParent;

    [SerializeField] GameObject m_corgettePrefab;

    [SerializeField] Transform m_corgetteSpawnPosition;

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
        GameObject corgette = Instantiate(m_corgettePrefab , m_corgetteSpawnPosition) as GameObject;

        if(m_projectilesParent != null)
        {
            corgette.transform.parent = m_projectilesParent.transform;
        }
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
				
			break;
			
			case CactusState.IDLE: 
				
			break;
		}
	}
}
