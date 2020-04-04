using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] int starCost = 100;

    [Header("Debug Only")]
    [SerializeField] int laneNumber;

    private void Start()
    {
        laneNumber = Mathf.FloorToInt(transform.position.y);
        FindObjectOfType<LevelController>().DefenderSpawned(this);
    }

    public int LaneNumber()
    {
        return laneNumber;
    }

    public int GetStarCost()
    {
        return starCost;
    }
}
