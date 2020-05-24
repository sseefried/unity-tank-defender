using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] int starCost = 100;
    [SerializeField] AnimationClip clip;


    [Header("Debug Only")]
    [SerializeField] int row;
    [SerializeField] int column;


    private void Awake()
    {
        row = Mathf.FloorToInt(transform.position.y);
        column = Mathf.FloorToInt(transform.position.x);
    }

    public int Row()
    {
        return row;
    }

    public int Column()
    {
        return column;
    }

    public int GetStarCost()
    {
        return starCost;
    }
        
}
