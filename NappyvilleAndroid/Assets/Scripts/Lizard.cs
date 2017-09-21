using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    Animator m_animator;
    bool m_isWalking;

    [Range(0.0f , 1.5f)] [SerializeField] float m_walkSpeed;


	void Start()
    {
	    m_animator = GetComponent<Animator>();
        StartCoroutine("AnimsRoutine");
	}

    void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        if(m_isWalking)
        {
            transform.Translate(Vector2.left * m_walkSpeed * Time.deltaTime);
        }
	}
	
	IEnumerator AnimsRoutine()
    {
        yield return new WaitForSeconds(3f);
        m_isWalking = true;
        m_animator.SetBool("Walk" , true);

        yield return new WaitForSeconds(6f);
        m_isWalking = false;
        m_animator.SetBool("Attack" , true);

        yield return new WaitForSeconds(3f);
        m_animator.SetBool("Attack" , false);

        StartCoroutine("AnimsRoutine");
    }
}
