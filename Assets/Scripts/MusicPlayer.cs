using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] public AudioClip startScreenMusic;
    [SerializeField] public AudioClip levelMusic;

    void Awake()
    {
        SetUpSingleton();
        audioSource = FindObjectOfType<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioSource oldAudioSource = audioSource;
        audioSource = FindObjectOfType<AudioSource>();
        if (audioSource != oldAudioSource)
        {
            oldAudioSource.Stop();
        }
    }

    private void SetUpSingleton()
    {
        int objectCount = FindObjectsOfType(GetType()).Length;
        if (objectCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

}
