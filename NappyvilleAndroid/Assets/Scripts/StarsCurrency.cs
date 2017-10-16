using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCurrency : MonoBehaviour
{
    //[SerializeField] float m_gameTime;
    [SerializeField] Text m_gameTimeLabel , m_starScoreLabel;

    public enum Status {SUCCESS , FAILURE};

    public int m_starsCount;

    void Start()
    {
        //m_gameTimeLabel = GameObject.Find("GameTime").GetComponent<Text>(); // Only for testing
        m_starScoreLabel = GetComponent<Text>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        //m_gameTime += Time.deltaTime; // Only for Testing
        //m_gameTimeLabel.text = Mathf.RoundToInt(m_gameTime).ToString(); // Only for testing
        m_starScoreLabel.text = m_starsCount.ToString();
    }

    void DecrementStars(int amount)
    {
        m_starsCount -= amount;
    }

    void IncrementStars(int amount)
    {
        m_starsCount += amount;
    }

    public Status UseStars(int amount)
    {
        if(m_starsCount >= amount)
        {
            m_starsCount -= amount;
            return Status.SUCCESS;
        }
        
        return Status.FAILURE;
    }
}
