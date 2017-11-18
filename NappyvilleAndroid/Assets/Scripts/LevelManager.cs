using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	bool m_adsMenuVisible , m_levelCompleteVisible;

    [SerializeField] BhanuEnemy[] m_bhanuEnemiesLeft;

	[SerializeField] BoxCollider2D m_gameCollider2D;

	[SerializeField] Color m_adsMenuNoButtonColour , m_adsMenuTextColour, m_adsMenuTextOutlineColour , m_adsMenuYesButtonColour , m_continueButtonColour;

	[SerializeField] Color m_levelCompleteTextColour , m_levelCompleteTextOutlineColour;

	[SerializeField] float m_loadTime;

	[SerializeField] Image m_adsMenuImage , m_adsMenuNoButtonImage , m_adsMenuYesButtonImage , m_continueButtonImage , m_fbProfilePicImage , m_levelCompleteImage;

	[SerializeField] GameObject m_adsMenuObj , m_fbLoggedInObj , m_fbLoggedOutObj , m_levelCompleteObj , m_pauseButtonObj , m_pauseMenuObj , m_quitMenuObj;

	[SerializeField] Outline m_adsMenuTextOutline , m_levelCompleteTextOutline;

	[SerializeField] Text m_adsMenuText , m_fbUsername , m_gameTimeDisplay , m_levelCompleteText;

    public float m_gameTime;
    public int m_currentSceneIndex;
    public int m_enemyKillTarget , m_totalEnemiesKilled = 0;
    public static Text m_notEnoughStarsText , m_selectPlayerText;

    void Start()
    {
		if(m_currentSceneIndex == 1)
		{
			FB.Init(FBSetInit , FBOnHideUnity);	
		}

		Time.timeScale = 1;

		Advertisement.Initialize("1607507" , false);

		if(m_adsMenuNoButtonImage != null)
		{
			m_adsMenuNoButtonColour = m_adsMenuNoButtonImage.color;
			m_adsMenuTextColour = m_adsMenuText.color;
			m_adsMenuTextOutlineColour = m_adsMenuTextOutline.effectColor;
			m_adsMenuYesButtonColour = m_adsMenuYesButtonImage.color;	
		}

		if(m_continueButtonImage != null)
		{
			m_continueButtonColour = m_continueButtonImage.color;
			m_levelCompleteTextColour = m_levelCompleteText.color;
			m_levelCompleteTextOutlineColour = m_levelCompleteTextOutline.effectColor;
		}

        m_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(m_currentSceneIndex > 1 && m_currentSceneIndex < 7)
        {
			m_notEnoughStarsText = GameObject.Find("NotEnoughStars").GetComponent<Text>();
			m_selectPlayerText = GameObject.Find("SelectPlayer").GetComponent<Text>();
            
			if(m_gameTimeDisplay != null) 
			{
				m_gameTimeDisplay = GameObject.Find("GameTimeDisplay").GetComponent<Text>();
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
		
	public void AdsNo()
	{
		SceneManager.LoadScene("07Lose");
	}

	public void AdsYes()
	{
		ShowRewardedVideo();
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

	void FBAuthCallBack(IResult result)
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
				FB.API("/me?fields=first_name" , HttpMethod.GET , FBUsernameDisplay);
				FB.API("/me/picture?type=square&height=480&width=480" , HttpMethod.GET , FBProfilePicDisplay);
			} 
			else 
			{
				//Debug.LogError("Sir Bhanu, Player hasn't logged in on Facebook");	
				m_fbLoggedInObj.SetActive(false);
				m_fbLoggedOutObj.SetActive(true);
			}
		}
	}

	public void FBLogin()
	{
		List<string> permissions = new List<string>();
		permissions.Add("public_profile");
		FB.LogInWithReadPermissions(permissions , FBAuthCallBack);
	}

	public void FBLogOut()
	{
		Debug.Log("Facebook Logout");
		FB.LogOut(); //Not Working
	}

	void FBOnHideUnity(bool isGameShown)
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

	void FBProfilePicDisplay(IGraphResult gResult)
	{
		if(gResult.Texture != null)
		{
			m_fbProfilePicImage.sprite = Sprite.Create(gResult.Texture , new Rect(0 , 0 , 480 , 480) , new Vector2());
		}
	}

	void FBSetInit()
	{
		if(FB.IsLoggedIn) 
		{
			//Debug.Log("Player Logged in"); //If you get 400 error, it means the user token of https://developers.facebook.com/tools/accesstoken/?app_id=142429536402184 you noted down is incorrect which is easy to resolve so not to worry
			m_fbLoggedInObj.SetActive(true);
			m_fbLoggedOutObj.SetActive(false);
			FB.API("/me?fields=first_name" , HttpMethod.GET , FBUsernameDisplay);
			FB.API("/me/picture?type=square&height=480&width=480" , HttpMethod.GET , FBProfilePicDisplay);
		} 
		else 
		{
			//Debug.LogError("Sir Bhanu, Player hasn't logged in on Facebook");	
			m_fbLoggedInObj.SetActive(false);
			m_fbLoggedOutObj.SetActive(true);
		}
	}

	void FBUsernameDisplay(IResult result)
	{
		if(result.Error == null)
		{
			//Debug.Log(result.ResultDictionary["first_name"]);
			m_fbUsername.text =  "Hi " + result.ResultDictionary["first_name"];
		}
	}

	void HandleShowResult (ShowResult result)
	{
		if(result == ShowResult.Finished) 
		{
			Debug.Log("Video completed - Offer a reward to the player");
			SceneManager.LoadScene(m_currentSceneIndex);
			Time.timeScale = 1;
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
		if(m_adsMenuObj != null) 
		{
			m_adsMenuObj.SetActive (true);
			m_adsMenuVisible = true;
			m_pauseButtonObj.SetActive (false);	
		} 
	}
}
