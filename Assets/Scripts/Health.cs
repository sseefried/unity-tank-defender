﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 2;
    [SerializeField] GameObject deathVFX;

    public void DealDamage(int damage)
    {
        Debug.Log("I've been hit!");
        health -= damage;
        if (health <= 0)
        {
            TriggerDeathVFX();
            FindObjectOfType<LevelController>().DefenderKilled(gameObject.GetComponent<Defender>());
            Destroy(gameObject);
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return; }
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 1f);
    }

}
