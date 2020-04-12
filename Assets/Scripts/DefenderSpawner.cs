using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    [Header("Configuration")]
    [Range(0, 1)]
    [SerializeField] float spriteTransparency = 0.5f;

    Defender defender;
    GameObject defenderParent;
    LevelController levelController;
    GameObject placeDefenderObject;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
        defenderParent = levelController.InstantiatedParent();

    }

    private void Update()
    {
        if (!defender) { return; }
        ShowPlaceDefenderObject();
    }

    private void ShowPlaceDefenderObject()
    {
        Vector2 square = GetSquare();
        if (MouseInGameArea() && !levelController.IsDefenderAt(Mathf.RoundToInt(square.y), Mathf.RoundToInt(square.x)) && placeDefenderObject)
        {
            Color color = new Color(1, 1, 1, spriteTransparency);
            var starDisplay = FindObjectOfType<StarDisplay>();
            if (!starDisplay.HaveEnoughStars(defender.GetStarCost())) // if not enough stars display as another colour
            {
                color = new Color(1f, 0f, 0f, spriteTransparency); 
            }
            ChangeColorOfPlaceDefenderObject(color);
            placeDefenderObject.SetActive(true);
            placeDefenderObject.transform.position = square;
        }
        else
        {
            placeDefenderObject.SetActive(false);
        }
    }

    private bool MouseInGameArea()
    {
        Vector2 square = GetSquare();
        if (square.x >= LevelController.MIN_COLUMN && square.x <= LevelController.MAX_COLUMN &&
            square.y >= LevelController.MIN_ROW && square.y <= LevelController.MAX_ROW)
        {
            return true;
        }
        return false;
    }


    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquare());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
        CreatePlaceDefenderObject();
    }

    private void CreatePlaceDefenderObject()
    {
        placeDefenderObject = Instantiate(defender.gameObject);
        placeDefenderObject.GetComponent<Animator>().enabled = false; // so that it doesn't animate
        placeDefenderObject.GetComponentInChildren<Collider2D>().enabled = false; // so that you can click in the game area
        Destroy(placeDefenderObject.GetComponent<Health>());
        Destroy(placeDefenderObject.GetComponent<Shooter>());
        Destroy(placeDefenderObject.GetComponent<Firing>());
        Destroy(placeDefenderObject.GetComponent<Defender>());
        Destroy(placeDefenderObject.GetComponentInChildren<Canvas>().gameObject);
        ShowPlaceDefenderObject();
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
        Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
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
        levelController.DefenderSpawned(Mathf.RoundToInt(worldPos.y), Mathf.RoundToInt(worldPos.x));
    }

    private void ChangeColorOfPlaceDefenderObject(Color color)
    {
        if (!placeDefenderObject) { return; }
        foreach (SpriteRenderer renderer in placeDefenderObject.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = color;
        }


    }

}