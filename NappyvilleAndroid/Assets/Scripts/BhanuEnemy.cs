using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuEnemy : MonoBehaviour
{
    [SerializeField] LevelManager m_levelManager;

    public float m_seenEverySecs;
    [Range(20 , 400)] public int m_hitpoints;

	void Start()
    {
        m_levelManager = FindObjectOfType<LevelManager>();
        StartCoroutine("DieRoutine");
	}

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(1f);

        if(m_hitpoints <= 0)
        {
            Destroy(gameObject);
        }

        if(transform.position.x < 0.4f)
        {
            m_levelManager.LoadScene("Lose");
        }

        StartCoroutine("DieRoutine");
    }
}
