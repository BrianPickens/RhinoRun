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
    private Text swipeSensitivityText;
    [SerializeField]
    private Text doubleSwipeSensitivityText;
    [SerializeField]
    private Image doubleSwipeImage;
    [SerializeField]
    private Sprite doubleSwipeOnSprite;
    [SerializeField]
    private Sprite doubleSwipeOffSprite;

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private GameObject creditsUI;

    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;
    public Action<bool> OnDoubleSwipeChange;
    public Action<int> OnSwipeSensitivityChange;
    public Action<int> OnDoubleSwipeSensitivityChange;

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

    public void OpenSettings()
    {
        Debug.Log("this happend");
        gameObject.SetActive(true);
        myAnimator.SetTrigger("MoveIn");
        Time.timeScale = 0;
    }

    public void ExitSettings()
    {
        myAnimator.SetTrigger("MoveOut");
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
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

    public void SwipeSensitivityChange(int _change)
    {
        swipeSensitivity += _change;
        if (swipeSensitivity < 0)
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
        if (doubleSwipeOn)
        {
            doubleSwipeOn = false;
            doubleSwipeImage.sprite = doubleSwipeOffSprite;
        }
        else
        {
            doubleSwipeOn = true;
            doubleSwipeImage.sprite = doubleSwipeOnSprite;
        }

        if (OnDoubleSwipeChange != null)
        {
            OnDoubleSwipeChange(doubleSwipeOn);
        }
    }

    public void DoubleSwipeSensitivtyChange(int _change)
    {
        doubleSwipeSensitivity += _change;
        if (doubleSwipeSensitivity < 0)
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

}
