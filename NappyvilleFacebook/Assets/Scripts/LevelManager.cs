﻿using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	bool m_levelCompleteVisible , m_loseLevelVisible;

    [SerializeField] BhanuEnemy[] m_bhanuEnemiesLeft;

	[SerializeField] BoxCollider2D m_gameCollider2D;

	[SerializeField] Color m_continueButtonColour , m_levelCompleteTextColour , m_levelCompleteTextOutlineColour , m_loseLevelTextColour , m_loseLevelTextOutlineColour , m_tryAgainButtonColour;

	[SerializeField] float m_loadTime;

	[SerializeField] Image m_loseLevelImage , m_continueButtonImage , m_levelCompleteImage , m_tryAgainButtonImage;

	[SerializeField] GameObject m_fbLoggedInObj , m_fbLoggedOutObj , m_loseLevelObj , m_levelCompleteObj , m_pauseButtonObj , m_pauseMenuObj , m_quitMenuObj;

	[SerializeField] Outline m_loseLevelTextOutline , m_levelCompleteTextOutline;

    [SerializeField] Text m_loseLevelText , m_gameTimeDisplay , m_levelCompleteText;

    public float m_gameTime;
    public int m_currentSceneIndex;
    public int m_enemyKillTarget , m_totalEnemiesKilled = 0;
    public static Text m_notEnoughStarsText;

    void Start()
    {
		FB.Init(SetInit , OnHideUnity);

		Time.timeScale = 1;

		if(m_continueButtonImage != null)
		{
			m_continueButtonColour = m_continueButtonImage.color;
			m_levelCompleteTextColour = m_levelCompleteText.color;
			m_levelCompleteTextOutlineColour = m_levelCompleteTextOutline.effectColor;
		}

		if(m_tryAgainButtonImage != null)
		{
			m_tryAgainButtonColour = m_tryAgainButtonImage.color;
			m_loseLevelTextColour = m_loseLevelText.color;
			m_loseLevelTextOutlineColour = m_loseLevelTextOutline.effectColor;
		}

        m_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(m_currentSceneIndex > 1 && m_currentSceneIndex < 7)
        {
			m_notEnoughStarsText = GameObject.Find("NotEnoughStars").GetComponent<Text>();
            
			if(m_gameTimeDisplay != null) 
			{
				m_gameTimeDisplay = GameObject.Find ("GameTimeDisplay").GetComponent<Text> ();
			} 
			else 
			{
				Debug.LogError("Sir Bhanu, there is no Game Time to Display as you use that only for testing");	
			}
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

		if(m_loseLevelVisible)
		{
			if(m_loseLevelImage.fillAmount < 1)
			{
				m_loseLevelImage.fillAmount += 0.01f;
			}

			if(m_loseLevelImage.fillAmount >= 1)
			{
				if(m_loseLevelTextColour.a < 1 && m_loseLevelTextOutlineColour.a < 1)
				{
					m_loseLevelTextColour.a += 0.01f;
					m_loseLevelText.color = m_loseLevelTextColour;

					m_loseLevelTextOutlineColour.a += 0.01f;
					m_loseLevelTextOutline.effectColor = m_loseLevelTextOutlineColour;
				}

				if(m_loseLevelTextColour.a >= 1)
				{
					if(m_tryAgainButtonColour.a < 1)
					{
						m_tryAgainButtonColour.a += 0.05f;
						m_tryAgainButtonImage.color = m_tryAgainButtonColour;
					}

					if(m_tryAgainButtonColour.a >= 1)
					{
						Time.timeScale = 0;
					}
				}
			}
		}

		if(m_levelCompleteVisible)
		{
			if(m_levelCompleteImage.fillAmount < 1)
			{
				m_levelCompleteImage.fillAmount += 0.01f;
			}

			if(m_levelCompleteImage.fillAmount >= 1)
			{
				if(m_levelCompleteTextColour.a < 1 && m_levelCompleteTextOutlineColour.a < 1)
				{
					m_levelCompleteTextColour.a += 0.01f;
					m_levelCompleteText.color = m_levelCompleteTextColour;

					m_levelCompleteTextOutlineColour.a += 0.01f;
					m_levelCompleteTextOutline.effectColor = m_levelCompleteTextOutlineColour;
				}

				if(m_levelCompleteTextColour.a >= 1)
				{
					if(m_continueButtonColour.a < 1)
					{
						m_continueButtonColour.a += 0.05f;
						m_continueButtonImage.color = m_continueButtonColour;
					}

					if(m_continueButtonColour.a >= 1)
					{
						Time.timeScale = 0;
					}
				}
			}
		}

        m_bhanuEnemiesLeft = FindObjectsOfType<BhanuEnemy>();

        if(m_currentSceneIndex > 1 && m_currentSceneIndex < 7)
        {
			m_gameTime += Time.deltaTime;

			if(m_gameTimeDisplay != null)
			{
				m_gameTimeDisplay.text = Mathf.RoundToInt(m_gameTime).ToString();
			}
        }

        if(m_bhanuEnemiesLeft.Length == 0 && m_totalEnemiesKilled >= m_enemyKillTarget)
        {
			LevelComplete();
        }
    }

	void AuthCallBack(IResult result)
	{
		if(result.Error != null) 
		{
			Debug.LogError("Sir Bhanu, there is an issue : " + result.Error);	
		} 
		else 
		{
			if(FB.IsLoggedIn) 
			{
				//Debug.Log("Player Logged in"); //If you get 400 error, it means the user token of https://developers.facebook.com/tools/accesstoken/?app_id=142429536402184 you noted down is incorrect which is easy to resolve so not to worry
				m_fbLoggedInObj.SetActive(true);
				m_fbLoggedOutObj.SetActive(false);
			} 
			else 
			{
				//Debug.LogError("Sir Bhanu, Player hasn't logged in on Facebook");	
				m_fbLoggedInObj.SetActive(false);
				m_fbLoggedOutObj.SetActive(true);
			}
		}
	}
		
	public void Continue()
	{
		if(m_currentSceneIndex < 6)
		{
			SceneManager.LoadScene(m_currentSceneIndex + 1);
		}

		else if(m_currentSceneIndex == 6)
		{
			SceneManager.LoadScene(m_currentSceneIndex + 2);
		}
	}

    public static void Disable(Text text)
    {
        text.enabled = false;
    }

    public static void Enable(Text text)
    {
        text.enabled = true;
    }

	public void FBLogin()
	{
		List<string> permissions = new List<string>();
		permissions.Add("public_profile");
		FB.LogInWithReadPermissions(permissions , AuthCallBack);
	}

	public void FBLogOut()
	{
		
	}
		
	void LevelComplete()
	{
		if(m_currentSceneIndex >= 2)
		{
			m_levelCompleteObj.SetActive(true);
			m_levelCompleteVisible = true;
			m_pauseButtonObj.SetActive(false);		
		}
	}

    public void LoadNextLevel()
    {
        if(m_currentSceneIndex < 1 && m_loadTime > 0)
        {
            SceneManager.LoadScene(m_currentSceneIndex + 1);
        }

		if(m_currentSceneIndex > 1 && m_currentSceneIndex < 6)
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
        SceneManager.LoadScene(name);
    }

	public void LoseLevel()
	{
		m_loseLevelObj.SetActive(true);
		m_loseLevelVisible = true;
		m_pauseButtonObj.SetActive(false);
	}

	public void Next() //Only for Testing
	{
		SceneManager.LoadScene(m_currentSceneIndex + 1);
	}

	void OnHideUnity(bool isGameShown)
	{
		if(!isGameShown) 
		{
			Time.timeScale = 0;
		} 
		else 
		{
			Time.timeScale = 1;	
		}
	}

	public void Pause()
	{
		m_gameCollider2D.enabled = false;
		m_pauseMenuObj.SetActive(true);
		Time.timeScale = 0;
	}

	public void Previous() //Only for Testing
	{
		SceneManager.LoadScene(m_currentSceneIndex - 1);
	}

	public void Quit()
	{
		if(m_currentSceneIndex > 1) 
		{
			m_pauseMenuObj.SetActive(false);
			m_quitMenuObj.SetActive(true);
		} 
		else 
		{
			Application.Quit();
		}
	}

	public void QuitNo()
	{
		m_pauseMenuObj.SetActive(true);
		m_quitMenuObj.SetActive(false);
	}

	public void QuitYes()
	{
		Debug.Log("QuitButton Pressed");
		Application.Quit();
	}

	public void Restart()
	{
		SceneManager.LoadScene(m_currentSceneIndex);
		Time.timeScale = 1;
	}

	public void Resume()
	{
		m_gameCollider2D.enabled = true;
		m_pauseMenuObj.SetActive(false);
		Time.timeScale = 1;
	}

	void SetInit()
	{
		if(FB.IsLoggedIn) 
		{
			//Debug.Log("Player Logged in"); //If you get 400 error, it means the user token of https://developers.facebook.com/tools/accesstoken/?app_id=142429536402184 you noted down is incorrect which is easy to resolve so not to worry
			m_fbLoggedInObj.SetActive(true);
			m_fbLoggedOutObj.SetActive(false);
		} 
		else 
		{
			//Debug.LogError("Sir Bhanu, Player hasn't logged in on Facebook");	
			m_fbLoggedInObj.SetActive(false);
			m_fbLoggedOutObj.SetActive(true);
		}
	}
		
	public void TryAgain()
	{
		SceneManager.LoadScene(2);
	}
}
