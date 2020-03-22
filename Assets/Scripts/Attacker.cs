using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject tankFiring;
    [Range(0f,3f)] [SerializeField] float speed = 1f;
    [SerializeField] float projectileDelay = 0.5f;
    [SerializeField] float fireDelayMin = 3f;
    [SerializeField] float fireDelayMax = 5f;

    float currentSpeed;

    GameObject currentTarget;


    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    private void Start()
    {
        StartCoroutine(FireContinuously());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * currentSpeed);
        //UpdateAnimationState();
    }

    //private void UpdateAnimationState()
    //{
    //    if (!currentTarget)
    //    {
    //        GetComponent<Animator>().SetBool("isAttacking", false);
    //    }
    //}

    public void SetMoving()
    {
        currentSpeed = speed;
    }


    public void Fire()
    {
        if (!tankFiring || !projectilePrefab) { return; }
        tankFiring.GetComponent<Animator>().SetTrigger("firing");
        StartCoroutine(WaitAndInstantiateProjectile());
    }

    // FIXME: Remove
    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    // FIXME: Remove
    public void StrikeCurrentTarget(int damage)
    {
        if (!currentTarget) { return; }
        Health health = currentTarget.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(damage);
        }
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController)
        {
            levelController.AttackerKilled();
        }
    }

    private IEnumerator WaitAndInstantiateProjectile()
    {
        yield return new WaitForSeconds(projectileDelay);
        Instantiate(projectilePrefab, tankFiring.transform);
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(fireDelayMin, fireDelayMax));
            Fire();
        }
    }

}


