using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Camera m_mainCamera;

    [Range(0.0f , 2.5f)] [SerializeField] float m_flySpeed;

    [SerializeField] GameObject m_currentTarget;

    [Range(20 , 100)] [SerializeField] int m_attack;

    Vector2 m_positionOnScreen;

	void Start()
    {
		m_mainCamera = FindObjectOfType<Camera>();
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);
        transform.Translate(Vector2.right * m_flySpeed * Time.deltaTime);

        if(m_positionOnScreen.x > 758.2f)
        {
            Destroy(gameObject);
        }
	}

    void CauseDamage()
    {
        if(m_currentTarget != null)
        {
            m_currentTarget.gameObject.GetComponent<AttackersHitpoints>().m_hitpoints -= m_attack;
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Sir Bhanu, there is no Defender target anymore to cause damage to");
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Corgette Collision with Enemy Successful"); //Working Fine
            m_currentTarget = tri2D.gameObject;
            CauseDamage();
        }
    }
}
