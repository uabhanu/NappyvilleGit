using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuPlayer : MonoBehaviour
{
    [Range(20 , 400)] public int m_hitpoints;

    [Range(0.0f , 10.0f)] [SerializeField] float m_lineDistance , m_lineStartingPoint;

	void Start()
    {
		StartCoroutine("DieRoutine");
	}

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        //EnemyDetected();
        Debug.DrawLine(new Vector2(transform.position.x + m_lineStartingPoint , transform.position.y) , new Vector2(transform.position.x + m_lineDistance , transform.position.y) , Color.blue);
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(1f);

        if(m_hitpoints <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine("DieRoutine");
    }

    bool EnemyDetected()
    {
        RaycastHit2D m_rayHit2D = Physics2D.Raycast(transform.position , Vector2.right);

        Debug.DrawLine(transform.position , Vector2.left);

        return m_rayHit2D;
    }
}
