using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] GameObject m_playerPrefab;

    [SerializeField] PlayerButton[] m_playerButtons;

    public static GameObject m_playerToSpawn;

	void Start()
    {
        m_playerButtons = FindObjectsOfType<PlayerButton>();   
	}

    public void SpriteColourToggle()
    {
        m_playerToSpawn = m_playerPrefab;

        foreach(PlayerButton playerButton in m_playerButtons)
        {
            playerButton.GetComponent<Image>().color = Color.black;
        }

        GetComponent<Image>().color = Color.white;
    }
}
