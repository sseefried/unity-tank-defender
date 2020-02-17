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

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }


    public void AttackerSpawned()
    {
        numberOfAttackers += 1;
    }

    public void AttackerKilled()
    {
        numberOfAttackers -= 1;
        if (numberOfAttackers <= 0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    public void LevelTimerFinished()
    {
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
}
