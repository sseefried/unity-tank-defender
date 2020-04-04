using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if (otherObject.GetComponent<Attacker>())
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
        if (otherObject.GetComponent<AttackerProjectile>())
        {
            Destroy(otherObject);
        }
    }
}
