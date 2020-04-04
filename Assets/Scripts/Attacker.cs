using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject tankFiring;
    [Range(0f,3f)] [SerializeField] float speed = 1f;
    [SerializeField] float projectileDelay = 0.5f;
    [SerializeField] float fireDelayMin = 3f;
    [SerializeField] float fireDelayMax = 5f;

    [Header("Debug only")]
    [SerializeField] int laneNumber = 0;

    float currentSpeed;
    bool inRange = false;
    LevelController levelController;

    private void Awake()
    {
        laneNumber = Mathf.FloorToInt(transform.position.y);
        levelController = FindObjectOfType<LevelController>();
        levelController.AttackerSpawned(this);       
    }

    private void Start()
    {
        StartCoroutine(FireContinuously());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * currentSpeed);
    }

    public int LaneNumber()
    {
        return laneNumber;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CoreGameArea")
        {
            inRange = true;
        }
    }

    public bool InRange()
    {
        return inRange;
    }

    public void SetMoving()
    {
        currentSpeed = speed;
    }


    public void Fire()
    {
        if (!tankFiring || !projectilePrefab || !inRange || !DefenderInLane()) { return; }
        tankFiring.GetComponent<Animator>().SetTrigger("firing");
        StartCoroutine(WaitAndInstantiateProjectile());
    }

    private bool DefenderInLane()
    {
        return FindObjectOfType<LevelController>().IsDefenderInLane(laneNumber);
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController)
        {
            levelController.AttackerKilled(this);
        }
    }

    private IEnumerator WaitAndInstantiateProjectile()
    {
        yield return new WaitForSeconds(projectileDelay);
        GameObject projectile = Instantiate(projectilePrefab, tankFiring.transform);
        projectile.transform.parent = levelController.InstantiatedParent().transform;
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


