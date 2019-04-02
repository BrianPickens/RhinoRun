using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    [SerializeField]
    private SettingsUI settingsUI;

    [SerializeField]
    private Text coinsText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private LoadingTransition loadingScreen;

    public Action OnPlayPress;
    public Action OnUpgradesPress;
    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;
    public Action<bool> OnDoubleSwipeChange;
    public Action<int> OnSwipeSensitivityChange;
    public Action<int> OnDoubleSwipeSensitivityChange;
    public Action OnGameCenterPress;
    public Action OnLeaderBoardPress;

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

        settingsUI.OnGameCenterPress = null;
        settingsUI.OnGameCenterPress += GameCenterPress;
    }

    public void InitializeSoundPreferences(bool _musicOn, bool _soundEffectsOn)
    {
        settingsUI.InitializeSoundPreferences(_musicOn, _soundEffectsOn);
    }

    public void InitializeControlPreferences(float _swipeSensitivity, float _doubleSwipeSensitivity, bool _doubleSwipeOn)
    {
        settingsUI.InitializeControlPreference(_swipeSensitivity, _doubleSwipeSensitivity, _doubleSwipeOn);
    }

    public void PlayPress()
    {
        PlayClickSound();
        loadingScreen.ShowLoading();
        if (OnPlayPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnPlayPress));
        }
    }

    public void UpgradesPress()
    {
        PlayClickSound();
        loadingScreen.ShowLoading();
        if (OnUpgradesPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnUpgradesPress));
        }
    }

    private IEnumerator SceneLoadDelay(Action _callback)
    {
        yield return new WaitForSeconds(1f);
        _callback();
    }

    public void OpenSettings()
    {
        settingsUI.OpenSettings();
    }

    public void UpdateCoinsDisplay(int _coins)
    {
        coinsText.text = "" + _coins;
    }

    public void UpdateScoreDisplay(int _score)
    {
        scoreText.text = _score + " Meters!";
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

    private void GameCenterPress()
    {
        if (OnGameCenterPress != null)
        {
            OnGameCenterPress();
        }
    }

    public void LeaderboardPress()
    {
        PlayClickSound();
        if (OnLeaderBoardPress != null)
        {
            OnLeaderBoardPress();
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
