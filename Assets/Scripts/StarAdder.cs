using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAdder : MonoBehaviour
{
    [Tooltip("Amount of stars this defender adds")]
    [SerializeField] int starAmountToAdd = 3;

    public void AddStars()
    {
        FindObjectOfType<StarDisplay>().AddStars(starAmountToAdd);
    }
}
