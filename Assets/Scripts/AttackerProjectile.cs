using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerProjectile : MonoBehaviour
{

    [SerializeField] int damage = 50;

    [Range (0f, 20f)] [SerializeField] float projectileSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * projectileSpeed * Time.deltaTime);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        var defender = other.GetComponent<Defender>();

        if (!health || !defender) { return; }
        health.DealDamage(damage);
        Destroy(gameObject);
    }

}
