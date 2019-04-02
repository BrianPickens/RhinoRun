﻿using System.Collections;
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
    private GameObject coinsHolder;

    [SerializeField]
    private PowerUpTimer shieldTimer;

    [SerializeField]
    private PowerUpTimer unlimitedChargeTimer;

    [SerializeField]
    private GameObject unlimitedChargeMeter;

    [SerializeField]
    private GameObject chargeMeter;

    [SerializeField]
    private GameObject settingsButton;

    [SerializeField]
    private GameObject chargeButton;

    [SerializeField]
    private RewardedAdPopup rewardedAdPopup;

    [SerializeField]
    private RewardedAdResult rewardedAdResult;

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
    public Action OnRewardedAdConfirmation;

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
        settingsUI.OnDoubleSwipeChange += DoubleSwipeChange;

        rewardedAdPopup.OnRewardedAdResponse = null;
        rewardedAdPopup.OnRewardedAdResponse += MultiplyCoinsResponse;

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

    public void DisplayEnding(int _points, int _distance, bool _isHighscore)
    {
        HideSettingsButton();
        HideCoins();
        HideChargeButton();
        HideChargeMeter();
        HideUnlimitedChargeTimer();
        HideShieldTimer();
        endingDisplay.OpenEndingDisplay(_points, _distance, _isHighscore);
    }

    public void SettingsButton()
    {
        settingsUI.OpenSettings();
    }

    public void MainMenuButton()
    {
        PlayClickSound();
        loadingScreen.ShowLoading();
        if (OnMenuPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnMenuPress));
        }
    }

    public void UpgradesButton()
    {
        PlayClickSound();
        loadingScreen.ShowLoading();
        if (OnUpgradesPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnUpgradesPress));
        }
    }

    public void ReplayButton()
    {
        PlayClickSound();
        loadingScreen.ShowLoading();
        if (OnReplayPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnReplayPress));
        }
    }

    public void MultiplyCoinsButton()
    {
        PlayClickSound();
        rewardedAdPopup.OpenRewardedAdPopup();
    }

    public void MultiplyCoinsResponse(bool _confirm)
    {
        PlayClickSound();
        if (_confirm)
        {
            if (OnRewardedAdConfirmation != null)
            {
                OnRewardedAdConfirmation();
            }
        }
    }

    public void DisplayRewardedAdResult(string _result)
    {
        rewardedAdResult.SetInfo(_result);
    }

    public void DisableMultiplierButton()
    {
        endingDisplay.DisableMultiplierButton();
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

    public void DisplayUnlimitedChargeMeter()
    {
        unlimitedChargeMeter.SetActive(true);
    }

    public void HideUnlimitedChargeMeter()
    {
        unlimitedChargeMeter.SetActive(false);
    }

    public void HideChargeButton()
    {
        chargeButton.gameObject.SetActive(false);
    }

    public void HideChargeMeter()
    {
        chargeMeter.gameObject.SetActive(false);
    }

    public void HideSettingsButton()
    {
        settingsButton.gameObject.SetActive(false);
    }

    public void HideCoins()
    {
        coinsHolder.gameObject.SetActive(false);
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

    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayClickSound();
        }
    }
}
