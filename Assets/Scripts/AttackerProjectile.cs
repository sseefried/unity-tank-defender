using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerProjectile : MonoBehaviour
{

    [Range (0f, 20f)] [SerializeField] float projectileSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }
}
