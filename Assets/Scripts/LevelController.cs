using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float timeToWait = 4f;
    [SerializeField] int startingStars = 500;

    [Header("Debug only")]
    [SerializeField] int numberOfAttackers = 0;
    [SerializeField] bool levelTimerFinished;

    Coroutine levelEndChecker;
    [SerializeField] SortedDictionary<int, int> defendersInLane;
    [SerializeField] SortedDictionary<int, int> attackersInLane;

    const string INSTANTIATED_PARENT_NAME = "Instantiated objects";
    GameObject instantiatedParent;

    const int MIN_LANE = 1;
    const int MAX_LANE = 5;

    private void Awake()
    {
        defendersInLane = new SortedDictionary<int, int>();
        attackersInLane = new SortedDictionary<int, int>();
        for (int i = MIN_LANE; i <= MAX_LANE; i++)
        {
            defendersInLane.Add(i, 0);
            attackersInLane.Add(i, 0);
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

    public void DefenderSpawned(Defender defender)
    {
        if (!defender) { return; }
        int lane = defender.LaneNumber();
        defendersInLane[lane] = defendersInLane[lane] + 1;
    }

    public void DefenderKilled(Defender defender)
    {
        if (!defender) { return; } 
        int lane = defender.LaneNumber();
        defendersInLane[lane] = defendersInLane[lane] - 1;
    }

    public bool IsDefenderInLane(int lane)
    {
        return defendersInLane[lane] > 0;
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
