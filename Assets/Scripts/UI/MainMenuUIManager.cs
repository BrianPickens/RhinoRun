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

    public Action OnPlayPress;
    public Action OnUpgradesPress;
    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;

    private void Start()
    {
        settingsUI.CloseSettings();

        settingsUI.OnSettingsClose = null;
        settingsUI.OnSettingsClose += CloseSettings;

        settingsUI.OnMusicChange = null;
        settingsUI.OnMusicChange += MusicChange;

        settingsUI.OnSoundEffectsChange = null;
        settingsUI.OnSoundEffectsChange += SoundEffectsChange;
    }

    public void InitializeSoundPreferences(bool _musicOn, bool _soundEffectsOn)
    {
        settingsUI.InitializeSoundPreferences(_musicOn, _soundEffectsOn);
    }

    public void PlayPress()
    {
        if (OnPlayPress != null)
        {
            OnPlayPress();
        }
    }

    public void OpenSettings()
    {
        settingsUI.gameObject.SetActive(true);
    }

    private void CloseSettings()
    {
        settingsUI.gameObject.SetActive(false);
    }

    public void UpgradesPress()
    {
        if (OnUpgradesPress != null)
        {
            OnUpgradesPress();
        }
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

}
