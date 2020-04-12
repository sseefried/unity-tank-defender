using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] GameObject gun;

    AttackerSpawner myLaneSpawner;
    Animator animator;
    LevelController levelController;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAttackerInLaneAndInRange())
        {          
            animator.SetTrigger("firing");
        }

    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawners)
        {
            bool isCloseEnough =
                Mathf.Abs(spawner.transform.position.y - transform.position.y)
                <= Mathf.Epsilon;
            if (isCloseEnough)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLaneAndInRange()
    {
        if (!myLaneSpawner) { return false;  }
        return myLaneSpawner.AttackerInRange();
    }

    public void Fire()
    {
        Projectile projectileObject =
            Instantiate(projectile, gun.transform.position,
            gun.transform.rotation);
        projectileObject.transform.parent = levelController.InstantiatedParent().transform;
    }



}
