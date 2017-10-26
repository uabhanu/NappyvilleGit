﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    float m_loadTime = 2f;

    [SerializeField] EnemySpawner m_enemySpawner;

    [SerializeField] EnemySpawner[] m_enemySpawners;

    [SerializeField] Text m_gameTimeLabel;

    public float m_gameTime;
    public int m_currentSceneIndex;
    public int m_enemyKillTarget , m_totalEnemiesKilled = 0;
    public static Text m_cantAffordMessage;

    void Start()
    {
        m_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        m_enemySpawner = FindObjectOfType<EnemySpawner>();
        m_enemySpawners = FindObjectsOfType<EnemySpawner>();

        if(m_currentSceneIndex > 1)
        {
            m_cantAffordMessage = GameObject.Find("CantAffordMessage").GetComponent<Text>();
            m_gameTimeLabel = GameObject.Find("GameTime").GetComponent<Text>(); // Only for testing
        }

        if(m_currentSceneIndex < 1)
        {
            Invoke("LoadNextLevel" , m_loadTime);    
        }
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(m_currentSceneIndex > 1)
        {
            m_gameTime += Time.deltaTime; // Only for Testing
            m_gameTimeLabel.text = Mathf.RoundToInt(m_gameTime).ToString(); // Only for testing
        }

        foreach(EnemySpawner enemySpawner in m_enemySpawners)
        {
            if(enemySpawner.transform.childCount == 0 && m_totalEnemiesKilled >= m_enemyKillTarget)
            {
                LoadNextLevel();
            }
        }
    }

    public void Quit()
    {
        Debug.Log("QuitButton Pressed");
        Application.Quit();
    }

    public static void Disable(Text text)
    {
        text.enabled = false;
    }

    public static void Enable(Text text)
    {
        text.enabled = true;
    }

    public void LoadNextLevel()
    {
        if(m_currentSceneIndex < 1 && m_loadTime > 0)
        {
            SceneManager.LoadScene(m_currentSceneIndex + 1);
        }

        if(m_currentSceneIndex > 1)
        {
            SceneManager.LoadScene(m_currentSceneIndex + 1);
        }

        if(m_currentSceneIndex == 6)
        {
            SceneManager.LoadScene(m_currentSceneIndex + 2);
        }
    }

    public void LoadScene(string name)
    {
        Debug.Log("StartButton Pressed");
        SceneManager.LoadScene(name);
    }
}
