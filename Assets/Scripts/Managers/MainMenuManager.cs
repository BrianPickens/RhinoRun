using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private MainMenuUIManager mainUI = null;

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

        if (SaveManager.Instance != null)
        {
            if (!SaveManager.Instance.GetTOSStatus())
            {
                mainUI.ShowTOS();
            }
        }

        mainUI.OnPlayPress = null;
        mainUI.OnPlayPress += PlayGame;

        mainUI.OnUpgradesPress = null;
        mainUI.OnUpgradesPress += OpenUpgrades;

        mainUI.OnMusicChange = null;
        mainUI.OnMusicChange += UpdateMusicPreference;

        mainUI.OnSoundEffectsChange = null;
        mainUI.OnSoundEffectsChange += UpdateSoundEffectsPreference;

        mainUI.OnGameCenterPress = null;
        mainUI.OnGameCenterPress += InitializeGameCenter;

        mainUI.OnLeaderBoardPress = null;
        mainUI.OnLeaderBoardPress += ShowLeaderBoard;

        mainUI.Init();

        InitializeUI();

#if UNITY_IOS
        if (!GameCenter.initialLogInAttempted)
        {
            InitializeGameCenter();
        }
#endif

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

    private void InitializeGameCenter()
    {
        GameCenter.AuthenticateUser();
    }

    private void ShowLeaderBoard()
    {
        GameCenter.ShowLeaderBoard();
    }

    private void PlayGame()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadSceneAsync("PlayScene");
        }
    }

    private void OpenUpgrades()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadSceneAsync("Upgrades");
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
        SaveManager.Instance.UpdateCoins(100000);
        mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
    }

    public void ClearData()
    {
        SaveManager.Instance.ClearPlayerPrefs();
    }
    //end debug options

}
