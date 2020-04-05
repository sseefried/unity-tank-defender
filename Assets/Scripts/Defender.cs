using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] int starCost = 100;
    [SerializeField] public Sprite staticSprite;

    [Header("Debug Only")]
    [SerializeField] int row;
    [SerializeField] int column;

    private void Start()
    {
        row = Mathf.FloorToInt(transform.position.y);
        column = Mathf.FloorToInt(transform.position.x);
        FindObjectOfType<LevelController>().DefenderSpawned(this);
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
