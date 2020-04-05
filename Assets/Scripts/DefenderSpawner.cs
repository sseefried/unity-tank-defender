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

    private void OnMouseOver()
    {
        if (!defender) { return; }
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector2 square = GetSquare();
        if (levelController.IsDefenderAt(Mathf.RoundToInt(square.y), Mathf.RoundToInt(square.x)))
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            spriteRenderer.gameObject.transform.position = square;
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = new Color(1, 1, 1, spriteTransparency);
        }
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
