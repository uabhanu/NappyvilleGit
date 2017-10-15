﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrophy : MonoBehaviour
{
    Animator m_animator;

    [HideInInspector] [Range(0.0f , 10.0f)] [SerializeField] float m_lineDistance , m_lineStartingPoint;

    [SerializeField] GameObject m_starPrefab;

	void Start()
    {
	    m_animator = GetComponent<Animator>();	
        StartCoroutine("AnimsRoutine");
	}
	
	IEnumerator AnimsRoutine()
    {
        yield return new WaitForSeconds(4f);
        m_animator.SetBool("Star" , true);

        yield return new WaitForSeconds(2.5f);
        m_animator.SetBool("Star" , false);

        StartCoroutine("AnimsRoutine");
    }

    void StarSpawn()
    {
        GameObject newStar = Instantiate(m_starPrefab , transform.position , transform.rotation) as GameObject;
        newStar.transform.position = new Vector2(transform.position.x , transform.position.y + 0.23f);
    }
}
