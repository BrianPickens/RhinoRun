﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUI : MonoBehaviour
{

    [SerializeField]
    private EndingDisplayUI endingDisplay = null;

    [SerializeField]
    private SettingsUI settingsUI = null;

    [SerializeField]
    private Text pointsDisplay = null;

    [SerializeField]
    private GameObject coinsHolder = null;

    [SerializeField]
    private PowerUpTimer shieldTimer = null;

    [SerializeField]
    private PowerUpTimer unlimitedChargeTimer = null;

    [SerializeField]
    private GameObject unlimitedChargeMeter = null;

    [SerializeField]
    private GameObject chargeMeter = null;

    [SerializeField]
    private GameObject settingsButton = null;

    [SerializeField]
    private GameObject chargeButton = null;

    [SerializeField]
    private GameObject leftButton = null;

    [SerializeField]
    private GameObject rightButton = null;

    [SerializeField]
    private RewardedAdPopup rewardedAdPopup = null;

    [SerializeField]
    private RewardedAdResult rewardedAdResult = null;

    [SerializeField]
    LoadingTransition loadingScreen = null;

    public Action OnMenuPress;
    public Action OnUpgradesPress;
    public Action OnReplayPress;
    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;
    public Action<bool> OnTutorialChange;
    public Action OnRewardedAdConfirmation;
    public Action OnGameCenterPress;

    public void Init()
    {
        loadingScreen.StartWithLoading();
        loadingScreen.HideLoading();

        settingsUI.OnMusicChange = null;
        settingsUI.OnMusicChange += MusicChange;

        settingsUI.OnSoundEffectsChange = null;
        settingsUI.OnSoundEffectsChange += SoundEffectsChange;

        settingsUI.OnGameCenterPress = null;
        settingsUI.OnGameCenterPress += GameCenterPress;

        settingsUI.OnTutorialChange = null;
        settingsUI.OnTutorialChange += TutorialChange;

        rewardedAdPopup.OnRewardedAdResponse = null;
        rewardedAdPopup.OnRewardedAdResponse += MultiplyCoinsResponse;

    }

    public void InitializeSoundPreferences(bool _musicOn, bool _soundEffectsOn)
    {
        settingsUI.InitializeSoundPreferences(_musicOn, _soundEffectsOn);
    }

    public void DisplayPoints(int _points)
    {
        if (_points == 0)
        {
            pointsDisplay.text = "" + _points;
        }
        else
        {
            string pointsString = _points.ToString("#,#");

            pointsDisplay.text = pointsString;
        }
    }

    public void InitializeTutorialStatus(bool _tutorialCompleted)
    {
        settingsUI.SetTutorialStatus(_tutorialCompleted);
    }

    public void DisplayEnding(int _points, int _distance, bool _isHighscore)
    {
        HideSettingsButton();
        HideCoins();
        HideControls();
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

    public void DisplayRewardedAdResult(string _result, bool _rewardGiven)
    {
        rewardedAdResult.SetInfo(_result, _rewardGiven);
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

    public void HideControls()
    {
        chargeButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
    }

    public void ShowArrows()
    {
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
    }

    public void ShowCharge()
    {
        chargeButton.gameObject.SetActive(true);
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

    private void TutorialChange(bool _tutorialOn)
    {
        if (OnTutorialChange != null)
        {
            OnTutorialChange(_tutorialOn);
        }
    }

    private void GameCenterPress()
    {
        if (OnGameCenterPress != null)
        {
            OnGameCenterPress();
        }
    }

    private void PlaySound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
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
