using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Level timer in seconds")]
    [SerializeField] float levelTime = 10;
    bool triggeredLevelFinished = false;
    Slider slider;

    private void Start()
    {
        slider = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggeredLevelFinished) { return;  }

        // between 0 and 1
        slider.value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = Time.timeSinceLevelLoad >= levelTime;

        if (timerFinished)
        {
            FindObjectOfType<LevelController>().LevelTimerFinished();
            triggeredLevelFinished = true;         
        }
    }
}
