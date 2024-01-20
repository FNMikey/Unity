using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    public AudioClip musicForScene0And2;
    public AudioClip musicForScene1;

    private AudioSource audioSource;
    private int lastSceneIndex = -1; // variable to store the last played scene index

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
            PlayCorrectMusicForScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayCorrectMusicForScene(scene.buildIndex);
    }

    void PlayCorrectMusicForScene(int sceneIndex)
    {
        if (sceneIndex == 1)
        {
            // Check if the music is already playing for this scene
            if (audioSource.clip != musicForScene1 || lastSceneIndex != sceneIndex)
            {
                // Play music for scene 1
                audioSource.clip = musicForScene1;
                audioSource.Play();
            }
        }

        lastSceneIndex = sceneIndex; // update the last scene index
    }
}
