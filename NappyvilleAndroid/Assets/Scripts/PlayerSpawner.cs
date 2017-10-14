using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    GameObject m_playerParent;

    [SerializeField] Camera m_myCamera;

	void Start()
    {
		m_playerParent = GameObject.Find("PlayerParent");

        if(m_playerParent == null)
        {
            m_playerParent = new GameObject("PlayerParent");
        }
	}
	
	void OnMouseDown()
    {
        GameObject playerToSpawn = PlayerButton.m_playerToSpawn;
        Quaternion defaultRot = Quaternion.identity;
        Vector2 rawWorldPos = WorldPointOfMouseClick();
        Vector2 roundedPos = SnapToGrid(rawWorldPos);

        if(playerToSpawn != null)
        {
            GameObject newPlayer = Instantiate(playerToSpawn , roundedPos , defaultRot) as GameObject;
            newPlayer.transform.parent = m_playerParent.transform;
        }
        else
        {
            Debug.LogError("Sir Bhanu, no player has been selected yet. Please select the Player you want to Spawn");
        }
	}

    Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(WorldPointOfMouseClick().x);
        float newY = Mathf.RoundToInt(WorldPointOfMouseClick().y);
        return new Vector2(newX , newY);
    }

    Vector2 WorldPointOfMouseClick()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float distanceFromCamera = 1f;

        Vector3 weirdTriplet = new Vector3(mouseX , mouseY , distanceFromCamera);
        Vector2 worldPos = m_myCamera.ScreenToWorldPoint(weirdTriplet);

        return worldPos;
    }
}
