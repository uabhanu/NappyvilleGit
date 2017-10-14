using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonReset : MonoBehaviour
{
    [SerializeField] PlayerButton[] m_playerButtons;

	void Start()
    {
		m_playerButtons = FindObjectsOfType<PlayerButton>();
	}
	
	void OnMouseDown()
    {
		foreach(PlayerButton playerButton in m_playerButtons)
        {
            playerButton.GetComponent<Image>().color = Color.black;
        }
	}
}
