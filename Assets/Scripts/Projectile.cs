 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(0f, 5f)] [SerializeField] float projectileSpeed = 5f;
    [SerializeField] int damage = 1;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        var attacker = other.GetComponent<Attacker>();

        if (!health || !attacker) { return;  }
        health.DealDamage(damage);
        Destroy(gameObject);
    }
}
 