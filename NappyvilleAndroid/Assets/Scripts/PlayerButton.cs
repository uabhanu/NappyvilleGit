using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] PlayerButton[] m_playerButtons;

	void Start()
    {
	    m_playerButtons = FindObjectsOfType<PlayerButton>();   
	}
	
	public void SpriteColourToggle()
    {
        foreach(PlayerButton playerButton in m_playerButtons)
        {
            playerButton.GetComponent<Image>().color = Color.black;
        }

        GetComponent<Image>().color = Color.white;
    }
}
