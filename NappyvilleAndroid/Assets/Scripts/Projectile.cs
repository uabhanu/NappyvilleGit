using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(0.0f , 100f)] [SerializeField] float m_flySpeed;

    [SerializeField] GameObject m_currentTarget;

    [Range(0 , 1000)] [SerializeField] int m_attack;

    Vector2 m_positionOnScreen;

	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        transform.Translate(Vector2.right * m_flySpeed * Time.deltaTime);

        if(transform.position.x > 11.05f)
        {
            Destroy(gameObject);
        }
	}

    void CauseDamage()
    {
        if(m_currentTarget != null)
        {
            m_currentTarget.gameObject.GetComponent<BhanuEnemy>().m_hitpoints -= m_attack;
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
