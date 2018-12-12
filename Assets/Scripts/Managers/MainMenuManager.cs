using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private MainMenuUIManager mainUI;

    private void Start()
    {
        Init();   
    }

    private void Init()
    {

        if (InitializationManager.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Initialization");
        }

        mainUI.OnPlayPress = null;
        mainUI.OnPlayPress += PlayGame;

        mainUI.OnUpgradesPress = null;
        mainUI.OnUpgradesPress += OpenUpgrades;

        mainUI.OnMusicChange = null;
        mainUI.OnMusicChange += UpdateMusicPreference;

        mainUI.OnSoundEffectsChange = null;
        mainUI.OnSoundEffectsChange += UpdateSoundEffectsPreference;

        mainUI.OnSwipeSensitivityChange = null;
        mainUI.OnSwipeSensitivityChange += UpdateSwipeSensitivity;

        mainUI.OnDoubleSwipeSensitivityChange = null;
        mainUI.OnDoubleSwipeSensitivityChange += UpdateDoubleSwipeSensitivity;

        mainUI.OnDoubleSwipeChange = null;
        mainUI.OnDoubleSwipeChange += UpdateDoubleSwipeToggle;

        mainUI.Init();

        InitializeUI();

    }

    private void InitializeUI()
    {

        float tempSwipeSensitivity = 0.2f;
        float tempDoubleSwipeSensitivity = 3f;
        bool tempDoubleSwipeOn = true;
        if (SaveManager.Instance != null)
        {
            mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
            mainUI.UpdateScoreDisplay(SaveManager.Instance.GetCurrentHighscore());
            tempSwipeSensitivity = SaveManager.Instance.GetSwipeSensitivity();
            tempDoubleSwipeSensitivity = SaveManager.Instance.GetDoubleSwipeSensitivity();
            tempDoubleSwipeOn = SaveManager.Instance.GetDoubleSwipeStatus();
        }
        mainUI.InitializeControlPreferences(tempSwipeSensitivity, tempDoubleSwipeSensitivity, tempDoubleSwipeOn);

        bool musicOn = true;
        bool soundEffectsOn = true;
        if (SoundManager.Instance != null)
        {
            musicOn = SoundManager.Instance.MusicOn;
            soundEffectsOn = SoundManager.Instance.SoundEffectsOn;
        }
        mainUI.InitializeSoundPreferences(musicOn, soundEffectsOn);

    }

    private void PlayGame()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("PlayScene");
        }
    }

    private void OpenUpgrades()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("Upgrades");
        }
    }

    private void UpdateMusicPreference(bool _musicOn)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UpdateMusicPreference(_musicOn);
        }
    }

    private void UpdateSoundEffectsPreference(bool _soundEffectsOn)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UpdateSoundEffectPreference(_soundEffectsOn);
        }
    }

    private void UpdateSwipeSensitivity(int _change)
    {
        if (SaveManager.Instance != null)
        {
            float _newSensitivity = _change / 10f;
            SaveManager.Instance.SetSwipeSensitivity(_newSensitivity);
        }
    }

    private void UpdateDoubleSwipeSensitivity(int _change)
    {
        if (SaveManager.Instance != null)
        {
            float _newSensitivity = (_change / 10f) + 2.5f;
            SaveManager.Instance.SetDoubleSwipeSensitivity(_newSensitivity);
        }
    }

    private void UpdateDoubleSwipeToggle(bool _change)
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SetDoubleSwipe(_change);
        }
    }

    //debug options
    public void ResetMoney()
    {
        SaveManager.Instance.ResetMoney();
        mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
    }

    public void ResetScore()
    {
        SaveManager.Instance.ResetScore();
        mainUI.UpdateScoreDisplay(SaveManager.Instance.GetCurrentHighscore());
    }

    public void AddMoney()
    {
        SaveManager.Instance.UpdateCoins(500);
        mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
    }
    //end debug options

}
