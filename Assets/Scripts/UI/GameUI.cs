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
    private SettingsUI settingsUI;

    [SerializeField]
    private Text pointsDisplay;

    [SerializeField]
    private PowerUpTimer shieldTimer;

    [SerializeField]
    private PowerUpTimer unlimitedChargeTimer;

    [SerializeField]
    LoadingTransition loadingScreen;

    public Action OnMenuPress;
    public Action OnUpgradesPress;
    public Action OnReplayPress;
    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;
    public Action<bool> OnDoubleSwipeChange;
    public Action<int> OnSwipeSensitivityChange;
    public Action<int> OnDoubleSwipeSensitivityChange;

    public void Init()
    {
        loadingScreen.StartWithLoading();
        loadingScreen.HideLoading();

        settingsUI.OnMusicChange = null;
        settingsUI.OnMusicChange += MusicChange;

        settingsUI.OnSoundEffectsChange = null;
        settingsUI.OnSoundEffectsChange += SoundEffectsChange;

        settingsUI.OnSwipeSensitivityChange = null;
        settingsUI.OnSwipeSensitivityChange += SwipeSensivityChange;

        settingsUI.OnDoubleSwipeSensitivityChange = null;
        settingsUI.OnDoubleSwipeSensitivityChange += DoubleSwipeSensitivityChange;

        settingsUI.OnDoubleSwipeChange = null;
        settingsUI.OnDoubleSwipeChange = DoubleSwipeChange;

    }

    public void InitializeSoundPreferences(bool _musicOn, bool _soundEffectsOn)
    {
        settingsUI.InitializeSoundPreferences(_musicOn, _soundEffectsOn);
    }

    public void InitializeControlPreferences(float _swipeSensitivity, float _doubleSwipeSensitivity, bool _doubleSwipeOn)
    {
        settingsUI.InitializeControlPreference(_swipeSensitivity, _doubleSwipeSensitivity, _doubleSwipeOn);
    }

    public void DisplayPoints(int _points)
    {
        pointsDisplay.text = "" + _points;
    }

    public void DisplayEnding(int _points, int _distance)
    {
        endingDisplay.OpenEndingDisplay(_points, _distance);
    }

    public void SettingsButton()
    {
        settingsUI.OpenSettings();
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

    public void DisplayShieldTimer( )
    {
        shieldTimer.DisplayTimer();
    }

    public void UpdateShieldTimer(float _fillAmount)
    {
        shieldTimer.UpdateTimer(_fillAmount);
    }

    public void HideShieldTimer()
    {
        shieldTimer.EndTimer();
    }

    public void DisplayUnlimitedChargeTimer( )
    {
        unlimitedChargeTimer.DisplayTimer();
    }

    public void UpdateUnlimitedChargeTimer(float _fillAmount)
    {
        unlimitedChargeTimer.UpdateTimer(_fillAmount);
    }

    public void HideUnlimitedChargeTimer()
    {
        unlimitedChargeTimer.EndTimer();
    }

    private IEnumerator SceneLoadDelay(Action _callback)
    {
        yield return new WaitForSeconds(1f);
        _callback();
    }


    private void MusicChange(bool _musicOn)
    {
        if (OnMusicChange != null)
        {
            OnMusicChange(_musicOn);
        }
    }

    private void SoundEffectsChange(bool _soundEffectsOn)
    {
        if (OnSoundEffectsChange != null)
        {
            OnSoundEffectsChange(_soundEffectsOn);
        }
    }

    private void SwipeSensivityChange(int _change)
    {
        if (OnSwipeSensitivityChange != null)
        {
            OnSwipeSensitivityChange(_change);
        }
    }

    private void DoubleSwipeSensitivityChange(int _change)
    {
        if (OnDoubleSwipeSensitivityChange != null)
        {
            OnDoubleSwipeSensitivityChange(_change);
        }
    }

    private void DoubleSwipeChange(bool _change)
    {
        if (OnDoubleSwipeChange != null)
        {
            OnDoubleSwipeChange(_change);
        }
    }

}
