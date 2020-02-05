using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;

    [SerializeField] Attacker attackerPrefab;
    [SerializeField] float minDelay = 1f;
    [SerializeField] float maxDelay = 5f;

    // If Start is a Coroutine then it automatically has an implicit
    // StartCoroutine put around it.
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            SpawnAttacker();
                            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAttacker()
    {
        Attacker newAttacker =
            Instantiate(attackerPrefab,
                        transform.position,
                        transform.rotation) as Attacker;

        newAttacker.transform.parent = transform;  
    }
}
