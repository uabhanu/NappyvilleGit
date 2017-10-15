using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsScore : MonoBehaviour
{
    //Star m_star;

    [SerializeField] int m_starsCount;

    [SerializeField] Text m_starScoreLabel;

    void Start()
    {
        m_starScoreLabel = GetComponent<Text>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        //m_star = FindObjectOfType<Star>();

        //m_starScore = m_star.m_starScore;     
    }

    void DecrementStars(int amount)
    {
        m_starsCount -= amount;
        m_starScoreLabel.text = m_starsCount.ToString();
    }

    void IncrementStars(int amount)
    {
        m_starsCount += amount;
        m_starScoreLabel.text = m_starsCount.ToString();
    }
}
