using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    GameObject m_players;

    [SerializeField] PlayerButton[] m_playerButtons;

	void Start()
    {
        m_players = GameObject.Find("Players");

        if(m_players == null)
        {
            m_players = new GameObject("Players");
        }

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
