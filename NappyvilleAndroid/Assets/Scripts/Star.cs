using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    BoxCollider2D m_starCollider2D;

    //Star movement and disappearing logic coming soon

	void Start()
    {
		m_starCollider2D = GetComponent<BoxCollider2D>();
	}
	
	void Update()
    {
		
	}
}
