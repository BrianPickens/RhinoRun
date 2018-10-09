using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private EndingDisplayUI endingDisplay;

    [SerializeField]
    private Text pointsDisplay;

    [SerializeField]
    private Text distanceDisplay;

    public Action OnMenuPress;
    public Action OnUpgradesPress;
    public Action OnReplayPress;

    public void DisplayPoints(int _points)
    {
        pointsDisplay.text = "" + _points;
    }

    public void DisplayDistance(int _distance)
    {
        distanceDisplay.text = "" + _distance;
    }

    public void DisplayEnding(int _points, int _distance)
    {
        endingDisplay.DisplayEnding(_points, _distance);
    }

    public void MainMenuButton()
    {
        if (OnMenuPress != null)
        {
            OnMenuPress();
        }
    }

    public void UpgradesButton()
    {
        if (OnUpgradesPress != null)
        {
            OnUpgradesPress();
        }
    }

    public void ReplayButton()
    {
        if (OnReplayPress != null)
        {
            OnReplayPress();
        }
    }

}
