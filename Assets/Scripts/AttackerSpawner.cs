using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;

    [Header("Configuration")]
    [SerializeField] Attacker[] attackerPrefabs;
    [SerializeField] float minDelay = 1f;
    [SerializeField] float maxDelay = 5f;
    [SerializeField] float initialMaxDelay = 2f;

    LevelController levelController;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    // If Start is a Coroutine then it automatically has an implicit
    // StartCoroutine put around it.
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(0, initialMaxDelay));
        while (spawn)
        {
            SpawnAttacker();
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));                           
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    public bool AttackerInRange()
    {
        foreach (Attacker attacker in GetComponentsInChildren<Attacker>())
        {
            if (attacker.InRange())
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnAttacker()
    {
        var attackerIndex = Random.Range(0, attackerPrefabs.Length);
        Attacker attacker = Spawn(attackerPrefabs[attackerIndex]);
    }

    private Attacker Spawn(Attacker attacker)
    {
        Attacker newAttacker =
            Instantiate(attacker,
                        transform.position,
                        transform.rotation) as Attacker;

        newAttacker.transform.parent = transform;
        return newAttacker;
    }
}
