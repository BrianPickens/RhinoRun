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
    private Animator myAnimator = null;

    [SerializeField]
    private GameObject creditsUI = null;

    [SerializeField]
    private GameObject tosUI = null;

    [SerializeField]
    private Text tutorialText = null;

    public Action<bool> OnMusicChange;
    public Action<bool> OnSoundEffectsChange;
    public Action OnGameCenterPress;
    public Action<bool> OnTutorialChange;

    private bool musicOn;
    private bool soundEffectsOn;
    private bool tutorialCompleted;

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

    public void SetTutorialStatus(bool _tutorialCompleted)
    {
        tutorialCompleted = _tutorialCompleted;
        if (tutorialCompleted)
        {
            tutorialText.text = "Tutorial: OFF";
        }
        else
        {
            tutorialText.text = "Tutorial: ON";
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

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("http://brianpickensgames.com/PrivacyPolicy.html");
    }

    public void OpenTOS()
    {
        PlayClickSound();
        tosUI.SetActive(true);
    }

    public void CloseTOS()
    {
        PlayClickSound();
        tosUI.SetActive(false);
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

    public void TutorialChange()
    {
        PlayClickSound();
        if (tutorialCompleted)
        {
            tutorialCompleted = false;
            tutorialText.text = "Tutorial: ON";
        }
        else
        {
            tutorialCompleted = true;
            tutorialText.text = "Tutorial: Off";
        }

        if (OnTutorialChange != null)
        {
            OnTutorialChange(tutorialCompleted);
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
