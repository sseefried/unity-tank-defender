using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrefab;
    [SerializeField] AudioClip selectSound;

    public static Color32 unselectedColor = new Color32(100,100,100,255);

    private void Start()
    {
        LabelButtonWithCost();
        DimButton(this);
    }

    private void LabelButtonWithCost()
    {
        Text costText = GetComponentInChildren<Text>();
        costText.text = defenderPrefab.GetStarCost().ToString();
    }

    public void OnMouseDown()
    {
        foreach (DefenderButton button in FindObjectsOfType<DefenderButton>())
        {
            DimButton(button);
        }
        ChangeButtonColor(this, Color.white);
        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrefab);
        AudioSource.PlayClipAtPoint(selectSound, Camera.main.transform.position);
    }

    private static void DimButton(DefenderButton button)
    {
        ChangeButtonColor(button, unselectedColor);

    }

    private static void ChangeButtonColor(DefenderButton button, Color32 color)
    {
        Transform t = button.transform;
        t.Find("Image").GetComponent<SpriteRenderer>().color = color;
        t.Find("Button Graphic").GetComponent<SpriteRenderer>().color = color;
    }
}
