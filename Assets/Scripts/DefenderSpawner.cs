using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    [SerializeField] GameObject defenderPrefab;

    private void OnMouseDown()
    {
        Vector2 worldPos = GetSquareClicked();
        SpawnDefender(new Vector2(Mathf.Round(worldPos.x), Mathf.Round(worldPos.y)));
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return worldPos;
    }

    private void SpawnDefender(Vector2 pos)
    {
        GameObject newDefender = Instantiate(defenderPrefab, pos, Quaternion.identity) as GameObject;
    }
}
