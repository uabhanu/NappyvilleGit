using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	bool m_adsMenuVisible;

    [SerializeField] BhanuEnemy[] m_bhanuEnemiesLeft;

	[SerializeField] BoxCollider2D m_gameCollider2D;

	[SerializeField] Color m_adsMenuNoButtonColour , m_adsMenuTextColour, m_adsMenuTextOutlineColour , m_adsMenuYesButtonColour;

	[SerializeField] float m_loadTime;

	[SerializeField] Image m_adsMenuImage , m_adsMenuNoButtonImage , m_adsMenuYesButtonImage;

	[SerializeField] GameObject m_adsMenuObj , m_pauseMenuObj , m_quitMenuObj;

	[SerializeField] Outline m_adsMenuTextOutline;

    [SerializeField] Text m_adsMenuText , m_gameTimeDisplay;

    public float m_gameTime;
    public int m_currentSceneIndex;
    public int m_enemyKillTarget , m_totalEnemiesKilled = 0;
    public static Text m_notEnoughStarsText;

    void Start()
    {
		Advertisement.Initialize("1607507" , false);

		if(m_adsMenuNoButtonImage != null)
		{
			m_adsMenuNoButtonColour = m_adsMenuNoButtonImage.color;
			m_adsMenuTextColour = m_adsMenuText.color;
			m_adsMenuTextOutlineColour = m_adsMenuTextOutline.effectColor;
			m_adsMenuYesButtonColour = m_adsMenuYesButtonImage.color;	
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

		if(m_adsMenuVisible)
		{
			if(m_adsMenuImage.fillAmount < 1)
			{
				m_adsMenuImage.fillAmount += 0.01f;
			}

			if(m_adsMenuImage.fillAmount >= 1)
			{
				if(m_adsMenuTextColour.a < 1 && m_adsMenuTextOutlineColour.a < 1)
				{
					m_adsMenuTextColour.a += 0.01f;
					m_adsMenuText.color = m_adsMenuTextColour;

					m_adsMenuTextOutlineColour.a += 0.01f;
					m_adsMenuTextOutline.effectColor = m_adsMenuTextOutlineColour;
				}

				if(m_adsMenuTextColour.a >= 1)
				{
					if(m_adsMenuNoButtonColour.a < 1 && m_adsMenuYesButtonColour.a < 1)
					{
						m_adsMenuNoButtonColour.a += 0.05f;
						m_adsMenuNoButtonImage.color = m_adsMenuNoButtonColour;

						m_adsMenuYesButtonColour.a += 0.05f;
						m_adsMenuYesButtonImage.color = m_adsMenuYesButtonColour;
					}

					if(m_adsMenuNoButtonColour.a >= 1)
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
			Invoke("LoadNextLevel" , m_loadTime);
        }
    }
		
	public void AdsNo()
	{
		SceneManager.LoadScene("07Lose");
	}

	public void AdsYes()
	{
		ShowRewardedVideo();
	}

    public static void Disable(Text text)
    {
        text.enabled = false;
    }

    public static void Enable(Text text)
    {
        text.enabled = true;
    }

	void HandleShowResult (ShowResult result)
	{
		if(result == ShowResult.Finished) 
		{
			Debug.Log("Video completed - Offer a reward to the player");
			SceneManager.LoadScene(m_currentSceneIndex);
		}

		else if(result == ShowResult.Skipped) 
		{
			Debug.LogWarning("Video was skipped - Do NOT reward the player");
			SceneManager.LoadScene("07Lose");
		}

		else if(result == ShowResult.Failed) 
		{
			Debug.LogError("Video failed to show");
			SceneManager.LoadScene("07Lose");
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

	public void Next() //Only for Testing
	{
		SceneManager.LoadScene(m_currentSceneIndex + 1);
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

	void ShowRewardedVideo()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show("rewardedVideo" , options);
	}

	public void UnityAds()
	{
		m_adsMenuObj.SetActive(true);
		m_adsMenuVisible = true;
		m_pauseMenuObj.SetActive(false);
	}
}
