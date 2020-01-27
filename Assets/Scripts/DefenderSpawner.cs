using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defenderPrefab;

    private void OnMouseDown()
    {
        SpawnDefender(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defenderPrefab = defenderToSelect;
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

    private void SpawnDefender(Vector2 worldPos)
    {
        Defender newDefender = Instantiate(defenderPrefab, worldPos, Quaternion.identity) as Defender;
    }
}
