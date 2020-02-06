using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 worldPos)
    {
        var starDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = defender.GetStarCost();
        if (starDisplay.HaveEnoughStars(defenderCost) &&
            !IsDefenderAt(worldPos))
        {

            SpawnDefender(worldPos);
            starDisplay.SpendStars(defenderCost);
        }

    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
         return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);

    }

    private bool IsDefenderAt(Vector2 worldPos)
    {
        Defender[] defenders = FindObjectsOfType<Defender>();
        foreach (Defender defender in defenders)
        {
            if (AtSameCoordinate(defender.transform.position.y, worldPos.y) &&
                AtSameCoordinate(defender.transform.position.x, worldPos.x)) {
                return true;
            }
        }
        return false;
    }

    private bool AtSameCoordinate(float x1, float x2)
    {
        return Mathf.Abs(x1 - x2) < Mathf.Epsilon;
    }

    private void SpawnDefender(Vector2 worldPos)
    {
        Defender newDefender = Instantiate(defender, worldPos, Quaternion.identity) as Defender;
        Canvas canvas = FindObjectOfType<Canvas>();
        newDefender.transform.parent = canvas.transform;
    }
}
