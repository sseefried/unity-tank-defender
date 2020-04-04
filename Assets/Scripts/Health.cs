using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 2;
    [SerializeField] GameObject deathVFX;

    int startingHealth; 
    SimpleHealthBar healthBar;

    private void Start()
    {
        startingHealth = health;
        healthBar = GetComponentInChildren<SimpleHealthBar>();
        Debug.Log("healthbar: " + (healthBar == null));
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (healthBar)
        {
            healthBar.UpdateBar(health, startingHealth);
        }
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
