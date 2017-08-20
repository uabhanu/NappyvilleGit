using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] m_soundsArray;

    AudioSource m_audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);   
    }

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
	}
	
    void OnLevelWasLoaded(int level)
    {
        AudioClip currentLevelMusic = m_soundsArray[level];
        m_audioSource.clip = currentLevelMusic;
        m_audioSource.loop = true;
        m_audioSource.Play();
    }
}
