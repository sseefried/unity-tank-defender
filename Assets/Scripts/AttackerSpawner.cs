using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;

    [SerializeField] GameObject lizardPrefab;
    [SerializeField] float minDelay = 1f;
    [SerializeField] float maxDelay = 5f;

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            Spawn();
                            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        Instantiate(lizardPrefab, transform.position, Quaternion.identity);
    }
}
