using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float m_loadTime;

    void Start()
    {
        Invoke("LoadNextLevel" , m_loadTime);    
    }

    public void Quit()
    {
        Debug.Log("QuitButton Pressed");
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex < 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void LoadScene(string name)
    {
        Debug.Log("StartButton Pressed");
        SceneManager.LoadScene(name);
    }
}
