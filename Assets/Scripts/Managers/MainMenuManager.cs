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

        mainUI.Init();

        InitializeUI();

    }

    private void InitializeUI()
    {
        if (SaveManager.Instance != null)
        {
            mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
            mainUI.UpdateScoreDisplay(SaveManager.Instance.GetCurrentHighscore());
        }

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
