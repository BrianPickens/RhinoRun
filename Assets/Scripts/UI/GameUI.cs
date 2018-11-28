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

    [SerializeField]
    LoadingTransition loadingScreen;

    public Action OnMenuPress;
    public Action OnUpgradesPress;
    public Action OnReplayPress;

    public void Init()
    {
        loadingScreen.StartWithLoading();
        loadingScreen.HideLoading();
    }

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
        loadingScreen.ShowLoading();
        if (OnMenuPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnMenuPress));
        }
    }

    public void UpgradesButton()
    {
        loadingScreen.ShowLoading();
        if (OnUpgradesPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnUpgradesPress));
        }
    }

    public void ReplayButton()
    {
        loadingScreen.ShowLoading();
        if (OnReplayPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnReplayPress));
        }
    }

    private IEnumerator SceneLoadDelay(Action _callback)
    {
        yield return new WaitForSeconds(1f);
        _callback();
    }

}
