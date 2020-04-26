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
    [SerializeField] public AudioClip loseMusic;

    public const int START_SCREEN_MUSIC_INDEX = 0, LEVEL_MUSIC_INDEX = 1, WIN_MUSIC_INDEX = 2, LOSE_MUSIC_INDEX = 3;
  
    AudioClip[] audioClips;

    public static void MusicSet(int music_index)
    {
        MusicPlayer player = FindObjectOfType<MusicPlayer>();
        if (!player) { return; }
        player.SetMusic(music_index);
    }


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetUpSingleton();
        audioClips = new AudioClip[] { startScreenMusic, levelMusic, winMusic, loseMusic };
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

    public void SetMusic(int clipIndex)
    {
        SetMusic(audioClips[clipIndex]);
    }


}
