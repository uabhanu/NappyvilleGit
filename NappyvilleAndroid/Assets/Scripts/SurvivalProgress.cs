using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalProgress : MonoBehaviour
{
    LevelManager m_levelManager;

    [SerializeField] int m_timeToSurvive;

    public static Slider m_gameTimeSlider;

	void Start()
    {
        m_levelManager = FindObjectOfType<LevelManager>();
        m_gameTimeSlider = GetComponent<Slider>();
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_gameTimeSlider.value = m_levelManager.m_gameTime / m_timeToSurvive;
        Debug.Log(m_gameTimeSlider.value);

        if(m_gameTimeSlider.value == 1)
        {
            m_levelManager.LoadNextLevel(); // You have Survived. Play relevant audio, etc., here
        }
    }
}
