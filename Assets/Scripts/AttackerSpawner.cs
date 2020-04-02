using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;

    [SerializeField] Attacker[] attackerPrefabs;
    [SerializeField] float minDelay = 1f;
    [SerializeField] float maxDelay = 5f;
    [SerializeField] float initialMaxDelay = 2f;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAttacker()
    {
        var attackerIndex = Random.Range(0, attackerPrefabs.Length);
        Spawn(attackerPrefabs[attackerIndex]);
    }

    private void Spawn(Attacker attacker)
    {
        Attacker newAttacker =
            Instantiate(attacker,
                        transform.position,
                        transform.rotation) as Attacker;

        newAttacker.transform.parent = transform;
    }
}
