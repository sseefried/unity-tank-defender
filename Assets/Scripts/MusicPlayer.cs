using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] public AudioClip startScreenMusic;
    [SerializeField] public AudioClip levelMusic;
    [SerializeField] public AudioClip winMusic;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetUpSingleton();
    }

    void OnEnable()
    {
        // Required so that OnSceneLoaded gets called
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioSource oldAudioSource = audioSource;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != oldAudioSource && oldAudioSource != null)
        {
            oldAudioSource.Stop();
        }

        if (scene.name == "Win Screen")
        {
            SetMusic(winMusic);
        }
        if (scene.name.Contains("Level"))
        {
            SetMusic(levelMusic);
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
