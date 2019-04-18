using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsUI : MonoBehaviour
{

    [SerializeField]
    private Image musicImage = null;
    [SerializeField]
    private Sprite musicOnSprite = null;
    [SerializeField]
    private Sprite musicOffSprite = null;

    [SerializeField]
    private Image soundEffectsImage = null;
    [SerializeField]
    private Sprite soundEffectsOnSprite = null;
    [SerializeField]
    private Sprite soundEffectsOffSprite = null;

    [SerializeField]
    private Text swipeSensitivityText = null;
    [SerializeField]
    private Text doubleSwipeSensitivityText = null;
    [SerializeField]
    private Text doubleSwipeStateText = null;
    [SerializeField]
    private Sprite doubleSwipeOnSprite = null;
    [SerializeField]
    private Sprite doubleSwipeOffSprite = null;

    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private GameObject creditsUI = null;

    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;
    public Action<bool> OnDoubleSwipeChange;
    public Action<int> OnSwipeSensitivityChange;
    public Action<int> OnDoubleSwipeSensitivityChange;
    public Action OnGameCenterPress;

    private bool musicOn;
    private bool soundEffectsOn;
    private int swipeSensitivity;
    private int doubleSwipeSensitivity;
    private bool doubleSwipeOn;

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

    public void InitializeControlPreference(float _swipeSensitivity, float _doubleSwipeSensitivity, bool _doubleSwipeOn)
    {
        swipeSensitivity = Mathf.RoundToInt(_swipeSensitivity * 10f);
        swipeSensitivityText.text = "" + swipeSensitivity;
        doubleSwipeSensitivity = Mathf.RoundToInt((_doubleSwipeSensitivity - 2.5f)*10f);
        doubleSwipeSensitivityText.text = "" + doubleSwipeSensitivity;
        doubleSwipeOn = _doubleSwipeOn;
        if (doubleSwipeOn)
        {
            doubleSwipeStateText.text = "ON";
        }
        else
        {
            doubleSwipeStateText.text = "OFF";
        }
    }

    public void OpenSettings()
    {
        PlayClickSound();
        gameObject.SetActive(true);
        myAnimator.SetTrigger("MoveIn");
        Time.timeScale = 0;
    }

    public void ExitSettings()
    {
        PlayClickSound();
        myAnimator.SetTrigger("MoveOut");
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void MusicPress()
    {
        PlayClickSound();
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
        PlayClickSound();
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
        PlayClickSound();
        creditsUI.SetActive(true);
    }

    public void CloseCredits()
    {
        PlayClickSound();
        creditsUI.SetActive(false);
    }

    public void SwipeSensitivityChange(int _change)
    {
        PlayClickSound();
        swipeSensitivity += _change;
        if (swipeSensitivity <= 0)
        {
            swipeSensitivity = 1;
        }
        else if (swipeSensitivity > 10)
        {
            swipeSensitivity = 10;
        }
        swipeSensitivityText.text = "" + swipeSensitivity;

        if (OnSwipeSensitivityChange != null)
        {
            OnSwipeSensitivityChange(swipeSensitivity);
        }
    }

    public void DoubleSwipeToggle()
    {
        PlayClickSound();
        if (doubleSwipeOn)
        {
            doubleSwipeOn = false;
            doubleSwipeStateText.text = "OFF";
        }
        else
        {
            doubleSwipeOn = true;
            doubleSwipeStateText.text = "ON";
        }

        if (OnDoubleSwipeChange != null)
        {
            OnDoubleSwipeChange(doubleSwipeOn);
        }
    }

    public void DoubleSwipeSensitivtyChange(int _change)
    {
        PlayClickSound();
        doubleSwipeSensitivity += _change;
        if (doubleSwipeSensitivity <= 0)
        {
            doubleSwipeSensitivity = 1;
        }
        else if (doubleSwipeSensitivity > 10)
        {
            doubleSwipeSensitivity = 10;
        }
        doubleSwipeSensitivityText.text = "" + doubleSwipeSensitivity;

        if (OnDoubleSwipeSensitivityChange != null)
        {
            OnDoubleSwipeSensitivityChange(doubleSwipeSensitivity);
        }
    }

    public void GameCenterPress()
    {
        PlayClickSound();
        if (OnGameCenterPress != null)
        {
            OnGameCenterPress();
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
