using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float timeToWait = 4f;

    [Header("Debug only")]
    [SerializeField] int numberOfAttackers = 0;
    [SerializeField] bool levelTimerFinished;
    Coroutine levelEndChecker;

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        levelEndChecker = StartCoroutine(LevelEndChecker());
    }


    public void AttackerSpawned()
    {
        numberOfAttackers += 1;
    }

    public void AttackerKilled()
    {
        numberOfAttackers -= 1;
        CheckForEndOfLevel();
    }

    public void LevelTimerFinished()
    {
        Debug.Log("LevelController: LevelTimeFinished called");
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }

    private IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(timeToWait);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
        
    }

    private IEnumerator LevelEndChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckForEndOfLevel();
        }
    }

    private void CheckForEndOfLevel() {
        if (numberOfAttackers <= 0 && levelTimerFinished)
        {
            StopCoroutine(levelEndChecker);
            StartCoroutine(HandleWinCondition());
        }
    }
}
