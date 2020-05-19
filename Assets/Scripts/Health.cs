using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDestructable
{
    [SerializeField] int health = 2;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip deathSound;


    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    int startingHealth; 
    SimpleHealthBar healthBar;

    private void Start()
    {
        startingHealth = health;
        healthBar = GetComponentInChildren<SimpleHealthBar>();
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        PlayDamageSound();
        if (healthBar)
        {
            healthBar.UpdateBar(health, startingHealth);
        }
        if (health <= 0)
        {
            TriggerDeathVFX();
            PlayDeathSound();
            Defender defender = gameObject.GetComponent<Defender>();
            if (defender) // if it's a defender
            {
                FindObjectOfType<LevelController>().DefenderKilled(defender.Row(), defender.Column());
            }
            Destroy(gameObject);
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return; }
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 1f);
    }

    public void PlayDamageSound()
    {
        if (!damageSound) { return; }
        AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position);

    }
    public void PlayDeathSound()
    {
        if (!deathSound) { return; }
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);

    }

}
