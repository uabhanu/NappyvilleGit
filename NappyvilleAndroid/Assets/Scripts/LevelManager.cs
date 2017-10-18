using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float m_loadTime;

    [SerializeField] float m_gameTime;

    [SerializeField] Text m_gameTimeLabel;

    void Start()
    {
        m_gameTimeLabel = GameObject.Find("GameTime").GetComponent<Text>(); // Only for testing
        Invoke("LoadNextLevel" , m_loadTime);    
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        m_gameTime += Time.deltaTime; // Only for Testing
        m_gameTimeLabel.text = Mathf.RoundToInt(m_gameTime).ToString(); // Only for testing
    }

    public void Quit()
    {
        Debug.Log("QuitButton Pressed");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex < 1 && m_loadTime > 0)
        {
            Debug.Log(currentSceneIndex + 1);
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void LoadScene(string name)
    {
        Debug.Log("StartButton Pressed");
        SceneManager.LoadScene(name);
    }
}
