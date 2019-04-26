using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    [SerializeField]
    private SettingsUI settingsUI = null;

    [SerializeField]
    private Text coinsText = null;

    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private LoadingTransition loadingScreen = null;

    public Action OnPlayPress;
    public Action OnUpgradesPress;
    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;
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

        settingsUI.OnGameCenterPress = null;
        settingsUI.OnGameCenterPress += GameCenterPress;
    }

    public void InitializeSoundPreferences(bool _musicOn, bool _soundEffectsOn)
    {
        settingsUI.InitializeSoundPreferences(_musicOn, _soundEffectsOn);
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
        if (_coins == 0)
        {
            coinsText.text = "" + _coins;
        }
        else
        {
            string coinString = _coins.ToString("#,#");

            coinsText.text = coinString;
        }
    }

    public void UpdateScoreDisplay(int _score)
    {
        if (_score == 0)
        {
            scoreText.text = "" + _score + " Meters!";
        }
        else
        {
            string scoreString = _score.ToString("#,#");

            scoreText.text = scoreString + " Meters!";
        }
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
