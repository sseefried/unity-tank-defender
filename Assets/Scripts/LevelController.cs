﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float timeToWait = 5f;
    [SerializeField] int startingStars = 500;

    [Header("Debug only")]
    [SerializeField] int numberOfAttackers = 0;
    [SerializeField] bool levelTimerFinished;

    Coroutine levelEndChecker;
    [SerializeField] bool[][] defenders; // whether a defender is in a row or column
    [SerializeField] int[] attackersInLane;

    const string INSTANTIATED_PARENT_NAME = "Instantiated objects";
    GameObject instantiatedParent;

    public const int MIN_ROW = 1;
    public const int MAX_ROW = 5;
    const int NUM_ROWS = MAX_ROW - MIN_ROW + 1;

    public const int MIN_COLUMN = 1;
    public const int MAX_COLUMN = 9;
    const int NUM_COLUMNS = MAX_COLUMN - MIN_COLUMN + 1;

    private void Awake()
    {
        defenders = new bool[NUM_ROWS + MIN_ROW][]; // unused elements at beginning
        attackersInLane = new int[NUM_ROWS + MIN_ROW]; // unused elements at beginning
        for (int i = MIN_ROW; i <= MAX_ROW; i++)
        {
            defenders[i] = new bool[NUM_COLUMNS + MIN_COLUMN]; // unused elements at beginning
            for (int j = MIN_COLUMN; j <= MAX_COLUMN; j++)
            {
                defenders[i][j] = false;
            }
            attackersInLane[i] = 0;
        }
        instantiatedParent = GameObject.Find(INSTANTIATED_PARENT_NAME);
        if (!instantiatedParent)
        {
            instantiatedParent = new GameObject(INSTANTIATED_PARENT_NAME);
        }
    }


    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        levelEndChecker = StartCoroutine(LevelEndChecker());
        FindObjectOfType<StarDisplay>().SetStars(startingStars);
        foreach (Defender defender in FindObjectsOfType<Defender>())
        {
            DefenderSpawned(defender.Row(), defender.Column());
        }
    }

    public GameObject InstantiatedParent()
    {
        return instantiatedParent;
    }

    public void AttackerSpawned(Attacker attacker)
    {
        numberOfAttackers += 1;
        int lane = attacker.LaneNumber();
        attackersInLane[lane] = attackersInLane[lane] + 1;
    }

    public void AttackerKilled(Attacker attacker)
    {
        numberOfAttackers -= 1;
        int lane = attacker.LaneNumber();
        attackersInLane[lane] = attackersInLane[lane] - 1;
        CheckForEndOfLevel();
    }

    public void DefenderSpawned(int row, int column)
    {
        defenders[row][column] = true;
    }

    public void DefenderKilled(int row, int column)
    {
        defenders[row][column] = false;
    }

    public bool IsDefenderInLane(int lane)
    {
        for (int j = MIN_COLUMN; j <= MAX_COLUMN; j++)
        {
            if (defenders[lane][j])
            {
                return true;
            }
        }
        return false;
    }

    public bool IsDefenderAt(int row, int column)
    {
        return defenders[row][column];
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

    public void ForceWin()
    {
        StopCoroutine(levelEndChecker);
        StartCoroutine(HandleWinCondition());
    }

    private IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        MusicPlayer.MusicStop();
        yield return new WaitForSeconds(timeToWait);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        MusicPlayer.MusicSet(MusicPlayer.LOSE_MUSIC_INDEX);
        StopAttackerSounds();
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

    private void StopAttackerSounds()
    {
        foreach (Attacker attacker in FindObjectsOfType<Attacker>())
        {
            attacker.StopSound();
        }
    }

}
