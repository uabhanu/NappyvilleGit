using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] GameObject m_playerPrefab;

    [SerializeField] int m_playerCostValue;

    [SerializeField] PlayerButton[] m_playerButtons;

    [SerializeField] Text m_cantAffordMessage , m_playerCostDisplay;

    public static GameObject m_playerToSpawn;

	void Start()
    {
        m_cantAffordMessage = GameObject.Find("CantAffordMessage").GetComponent<Text>();
        m_playerCostDisplay = GetComponentInChildren<Text>();
        m_playerCostDisplay.text = m_playerCostValue.ToString();
        m_playerToSpawn = null;
        m_playerButtons = FindObjectsOfType<PlayerButton>();   
	}

    public void ResetSelection()
    {
        foreach(PlayerButton playerButton in m_playerButtons)
        {
            m_playerToSpawn = null;
            playerButton.GetComponent<Image>().color = Color.black;
        }
    }

    public void SpriteColourToggle()
    {
        m_cantAffordMessage.enabled = false;
        m_playerToSpawn = m_playerPrefab;

        foreach(PlayerButton playerButton in m_playerButtons)
        {
            playerButton.GetComponent<Image>().color = Color.black;
        }

        GetComponent<Image>().color = Color.white;
    }
}
