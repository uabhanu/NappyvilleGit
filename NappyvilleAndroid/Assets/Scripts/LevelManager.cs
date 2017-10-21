using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    float m_loadTime = 2f;
    int m_currentSceneIndex;

    [SerializeField] float m_gameTime;

    [SerializeField] Text m_gameTimeLabel;

    public static Text m_cantAffordMessage;

    void Start()
    {
        m_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(m_currentSceneIndex > 1)
        {
            m_cantAffordMessage = GameObject.Find("CantAffordMessage").GetComponent<Text>();
            m_gameTimeLabel = GameObject.Find("GameTime").GetComponent<Text>(); // Only for testing
        }
      
        Invoke("LoadNextLevel" , m_loadTime);    
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
    }

    public void Quit()
    {
        Debug.Log("QuitButton Pressed");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        if(m_currentSceneIndex < 1 && m_loadTime > 0)
        {
            Debug.Log(m_currentSceneIndex + 1);
            SceneManager.LoadScene(m_currentSceneIndex + 1);
        }
    }

    public void LoadScene(string name)
    {
        Debug.Log("StartButton Pressed");
        SceneManager.LoadScene(name);
    }
}
