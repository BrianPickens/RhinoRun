using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsUI : MonoBehaviour
{

    [SerializeField]
    private Image musicImage;
    [SerializeField]
    private Sprite musicOnSprite;
    [SerializeField]
    private Sprite musicOffSprite;

    [SerializeField]
    private Image soundEffectsImage;
    [SerializeField]
    private Sprite soundEffectsOnSprite;
    [SerializeField]
    private Sprite soundEffectsOffSprite;

    [SerializeField]
    private GameObject creditsUI;

    public Action OnSettingsClose;
    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;

    private bool musicOn;
    private bool soundEffectsOn;

    private void Start()
    {
        creditsUI.SetActive(false);
    }

    public void InitializeSoundPreferences(bool _musicOn, bool _soundEffectsOn)
    {
        musicOn = _musicOn;
        if (musicOn)
        {
            musicImage.sprite = musicOnSprite;
        }
        else
        {
            musicImage.sprite = musicOffSprite;
        }

        soundEffectsOn = _soundEffectsOn;
        if (soundEffectsOn)
        {
            soundEffectsImage.sprite = soundEffectsOnSprite;
        }
        else
        {
            soundEffectsImage.sprite = soundEffectsOffSprite;
        }

    }

    public void CloseSettings()
    {
        if (OnSettingsClose != null)
        {
            OnSettingsClose();
        }
    }

    public void MusicPress()
    {
        if (musicOn)
        {
            musicOn = false;
            musicImage.sprite = musicOffSprite;
        }
        else
        {
            musicOn = true;
            musicImage.sprite = musicOnSprite;
        }

        if (OnMusicChange != null)
        {
            OnMusicChange(musicOn);
        }
    }

    public void SoundEffectsPress()
    {
        if (soundEffectsOn)
        {
            soundEffectsOn = false;
            soundEffectsImage.sprite = soundEffectsOffSprite;
        }
        else
        {
            soundEffectsOn = true;
            soundEffectsImage.sprite = soundEffectsOnSprite;
        }

        if (OnSoundEffectsChange != null)
        {
            OnSoundEffectsChange(soundEffectsOn);
        }

    }

    public void OpenCredits()
    {
        creditsUI.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsUI.SetActive(false);
    }
    
    

}
