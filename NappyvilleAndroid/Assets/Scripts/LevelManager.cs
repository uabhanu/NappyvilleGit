using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    float m_loadTime = 2f;

    [SerializeField] BhanuEnemy[] m_bhanuEnemiesLeft;

    [SerializeField] Text m_gameTimeDisplay;

    public float m_gameTime;
    public int m_currentSceneIndex;
    public int m_enemyKillTarget , m_totalEnemiesKilled = 0;
    public static Text m_cantAffordMessage;

    void Start()
    {
        m_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(m_currentSceneIndex > 1)
        {
            m_cantAffordMessage = GameObject.Find("CantAffordMessage").GetComponent<Text>();
            m_gameTimeDisplay = GameObject.Find("GameTimeDisplay").GetComponent<Text>(); // Only for testing
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

        m_bhanuEnemiesLeft = FindObjectsOfType<BhanuEnemy>();

        if(m_currentSceneIndex > 1)
        {
            m_gameTime += Time.deltaTime; // Only for Testing
            m_gameTimeDisplay.text = Mathf.RoundToInt(m_gameTime).ToString(); // Only for testing
        }

        if(m_bhanuEnemiesLeft.Length == 0 && m_totalEnemiesKilled >= m_enemyKillTarget)
        {
            Invoke("LoadNextLevel" , 1f);
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
