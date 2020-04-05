using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] Sprite sprite;
    [Range(0, 1)]
    [SerializeField] float spriteTransparency = 0.5f;

    Defender defender;
    GameObject defenderParent;
    LevelController levelController;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
        defenderParent = levelController.InstantiatedParent();
    }
    private void Update()
    {
        if (!defender) { return; }
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector2 square = GetSquare();
        spriteRenderer.sprite = null;
        if (MouseInGameArea() && !levelController.IsDefenderAt(Mathf.RoundToInt(square.y), Mathf.RoundToInt(square.x)))
        {
            spriteRenderer.gameObject.transform.position = square;
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = new Color(1, 1, 1, spriteTransparency);
        }
    }

    private bool MouseInGameArea()
    {
        Vector2 square = GetSquare();
        return (square.x >= LevelController.MIN_COLUMN || square.x <= LevelController.MAX_COLUMN ||
                square.y >= LevelController.MIN_ROW || square.y <= LevelController.MAX_ROW);
    }


    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquare());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 worldPos)
    {
        var starDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = defender.GetStarCost();
        if (starDisplay.HaveEnoughStars(defenderCost))
        {
            SpawnDefender(worldPos);
            starDisplay.SpendStars(defenderCost);
        }

    }

    private Vector2 GetSquare()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        //Debug.Log("clickPos = " + clickPos.ToString() + ", worldPos " + worldPos.ToString());
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        //Debug.Log("Snapped pos = (" + newX.ToString() + "," + newY.ToString() + ")");
        return new Vector2(newX, newY);

    }

    private void SpawnDefender(Vector2 worldPos)
    {
        Defender newDefender = Instantiate(defender, worldPos, Quaternion.identity) as Defender;
        Canvas canvas = FindObjectOfType<Canvas>();
        newDefender.transform.parent = defenderParent.transform;
    }
}
