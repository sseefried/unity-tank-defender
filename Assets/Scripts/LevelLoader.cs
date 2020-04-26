using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int timeToWait = 4; // seconds

    [Header("Debug")]
    [SerializeField] int currentSceneIndex;

    private void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        currentSceneIndex = activeScene.buildIndex;
        if (currentSceneIndex == 0 )
        {
            StartCoroutine(WaitAndLoad());
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
        MusicPlayer.MusicSet(MusicPlayer.START_SCREEN_MUSIC_INDEX);
    }

    public void LoadNextScene()
    {
        currentSceneIndex++;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadYouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

}
