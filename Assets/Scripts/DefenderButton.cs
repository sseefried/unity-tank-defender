﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrefab;
    [SerializeField] AudioClip selectSound;

    private void Start()
    {
        LabelButtonWithCost();
    }

    private void LabelButtonWithCost()
    {
        Text costText = GetComponentInChildren<Text>();
        costText.text = defenderPrefab.GetStarCost().ToString();
    }

    public void OnMouseDown()
    {
        
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach (DefenderButton button in buttons)
        {
            button.GetComponentInChildren<SpriteRenderer>().color = new Color32(41,41,41,255);
        }
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrefab);
        AudioSource.PlayClipAtPoint(selectSound, Camera.main.transform.position);
    }
}
