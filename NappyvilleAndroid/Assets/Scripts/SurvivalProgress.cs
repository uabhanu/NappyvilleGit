using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalProgress : MonoBehaviour
{
    bool m_isEndOfLevel;
    LevelManager m_levelManager;

    [SerializeField] float m_levelSeconds;

    [SerializeField] Slider m_slider;

	void Start()
    {
        m_levelManager = FindObjectOfType<LevelManager>();
        m_slider = GetComponent<Slider>();
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_slider.value = Time.timeSinceLevelLoad / m_levelSeconds;

        if(Time.timeSinceLevelLoad >= m_levelSeconds && !m_isEndOfLevel)
        {
            m_levelManager.LoadScene("Win"); // You have Survived. Play relevant audio, etc., here
            m_isEndOfLevel = true;
        }
	}
}
