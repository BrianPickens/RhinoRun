using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { WaitingForStart, Playing, GameOver }
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Character character;

    [SerializeField]
    private LevelGenerator levelGenerator;

    [SerializeField]
    private PowerUpsManager powerUpsManager;

    [SerializeField]
    private GameUI gameUI;

    [SerializeField]
    private ParticleManager particleManager;

    private int currentCoinUpgradeLevel;

    private int currentMegaCoinUpgradeLevel;

    private int points;

    private int distance;

    private string currentLoadString;

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

        character.OnGameOver += GameOver;

        gameUI.OnMenuPress = null;
        gameUI.OnMenuPress += OpenMainMenu;

        gameUI.OnUpgradesPress = null;
        gameUI.OnUpgradesPress += OpenUpgrades;

        gameUI.OnReplayPress = null;
        gameUI.OnReplayPress += Replay;

        gameUI.DisplayPoints(0);

        gameUI.OnMusicChange = null;
        gameUI.OnMusicChange += UpdateMusicPreference;

        gameUI.OnSoundEffectsChange = null;
        gameUI.OnSoundEffectsChange += UpdateSoundEffectsPreference;

        gameUI.OnSwipeSensitivityChange = null;
        gameUI.OnSwipeSensitivityChange += UpdateSwipeSensitivity;

        gameUI.OnDoubleSwipeSensitivityChange = null;
        gameUI.OnDoubleSwipeSensitivityChange += UpdateDoubleSwipeSensitivity;

        gameUI.OnDoubleSwipeChange = null;
        gameUI.OnDoubleSwipeChange += UpdateDoubleSwipeToggle;

        levelGenerator.OnBlockPassed = null;
        levelGenerator.OnBlockPassed += BlockCompleted;

        powerUpsManager.Initialize();

        levelGenerator.Initialize(CollectableGained, powerUpsManager.GetAvaliablePowerUps());

        gameUI.Init();

        currentCoinUpgradeLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.CoinsUpgrade);
        currentMegaCoinUpgradeLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.MegaCoinUpgrade);

        float tempSwipeSensitivity = 0.2f;
        float tempDoubleSwipeSensitivity = 3f;
        bool tempDoubleSwipeOn = true;
        if (SaveManager.Instance != null)
        {
            tempSwipeSensitivity = SaveManager.Instance.GetSwipeSensitivity();
            tempDoubleSwipeSensitivity = SaveManager.Instance.GetDoubleSwipeSensitivity();
            tempDoubleSwipeOn = SaveManager.Instance.GetDoubleSwipeStatus();
        }
        gameUI.InitializeControlPreferences(tempSwipeSensitivity, tempDoubleSwipeSensitivity, tempDoubleSwipeOn);

        bool musicOn = true;
        bool soundEffectsOn = true;
        if (SoundManager.Instance != null)
        {
            musicOn = SoundManager.Instance.MusicOn;
            soundEffectsOn = SoundManager.Instance.SoundEffectsOn;
        }
        gameUI.InitializeSoundPreferences(musicOn, soundEffectsOn);

        character.Initialize(tempSwipeSensitivity, tempDoubleSwipeSensitivity, tempDoubleSwipeOn);

        StaticInfo.AddPlay();
    }

    private void GameOver()
    {
        levelGenerator.EndGame();
        bool isHighscore = SaveManager.Instance.CheckIfHighscoore(distance);

        gameUI.DisplayEnding(points, distance, isHighscore);
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.UpdateScore(distance);
            SaveManager.Instance.UpdateCoins(points);
        }
    }

    private void CollectableGained(CollectableType _collectableType)
    {
        switch (_collectableType)
        {
            case CollectableType.Gold:
                CoinCollected();
                break;

            case CollectableType.Charge:
                Debug.Log("got Charge");
                particleManager.CreateParticles(ParticleType.UnlimitedCharge, character.MyTransform.position);
                powerUpsManager.ActivatePowerUp(_collectableType);
                break;

            case CollectableType.Shield:
                Debug.Log("got shield");
                particleManager.CreateParticles(ParticleType.Shield, character.MyTransform.position);
                powerUpsManager.ActivatePowerUp(_collectableType);
                break;

            case CollectableType.MegaCoin:
                Debug.Log("got mega coin");
                MegaCoinCollected();
                particleManager.CreateParticles(ParticleType.Diamond, character.MyTransform.position);
                break;

            case CollectableType.Stamina:
                Debug.Log("got stamina");
                particleManager.CreateParticles(ParticleType.RhinoSnax, character.MyTransform.position);
                powerUpsManager.ActivatePowerUp(_collectableType);
                break;

            default:
                Debug.LogError("Unknown Collectable");
                break;
        }

    }

    private void CoinCollected()
    {
        int tempPoints = 0;
        switch (currentCoinUpgradeLevel)
        {
            case 0:
                tempPoints = 10;
                particleManager.CreateParticles(ParticleType.Bronze, character.MyTransform.position);
                break;

            case 1:
                tempPoints = 15;
                particleManager.CreateParticles(ParticleType.Silver, character.MyTransform.position);
                break;

            case 2:
                tempPoints = 20;
                particleManager.CreateParticles(ParticleType.Gold, character.MyTransform.position);
                break;

            default:
                tempPoints = 10;
                Debug.LogError("Somethign wrong in CoinCollected");
                break;
        }
        points += tempPoints;
        gameUI.DisplayPoints(points);
    }

    private void MegaCoinCollected()
    {
        int tempPoints = 0;
        switch (currentMegaCoinUpgradeLevel)
        {
            case 0:
                tempPoints = 0;
                break;

            case 1:
                tempPoints = 25;
                break;

            case 2:
                tempPoints = 50;
                break;

            case 3:
                tempPoints = 75;
                break;

            case 4:
                tempPoints = 100;
                break;

            default:
                tempPoints = 0;
                Debug.LogError("invalid megacoin collected value");
                break;
        }

        points += tempPoints;
        gameUI.DisplayPoints(points);
    }

    private void BlockCompleted()
    {
        distance++;
    }

    private void OpenMainMenu()
    {
        currentLoadString = "MainMenu";
        CheckForAd();
    }

    private void OpenUpgrades()
    {
        currentLoadString = "Upgrades";
        CheckForAd();
    }

    private void Replay()
    {
        currentLoadString = "PlayScene";
        CheckForAd();
    }

    private void CheckForAd()
    {
        int numPlays = StaticInfo.GetNumberOfPlays();
        if (numPlays > 0 && numPlays % 2 == 0)
        {
            if (UnityAds.CheckForAd())
            {
                UnityAds.ShowAd(AdCompleted);
            }
            else
            {
                LoadScene(currentLoadString);
            }
        }
        else
        {
            LoadScene(currentLoadString);
        }
    }

    private void CheckForRewardedAd()
    {
        if (UnityAds.CheckForRewardedAd())
        {

        }
        else
        {

        }
    }

    private void AdCompleted(AdType _adType, bool _completed)
    {
        switch (_adType)
        {
            case AdType.Normal:
                if (_completed)
                {
                    Debug.Log("normal ad completed");
                }
                else
                {
                    Debug.Log("normal ad fialed");
                }

                LoadScene(currentLoadString);

                break;

            case AdType.Rewarded:

                if (_completed)
                {
                    Debug.Log("rewarded ad completed");
                }
                else
                {
                    Debug.Log("rewarded ad failed or skiped");
                }

                break;

            default:
                Debug.Log("unknown ad type reutrned to ad completed");
                break;
        }

    }



    private void LoadScene(string _scene)
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene(_scene);
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
            character.SetSwipeSensitivity(_newSensitivity);
            SaveManager.Instance.SetSwipeSensitivity(_newSensitivity);
        }
    }

    private void UpdateDoubleSwipeSensitivity(int _change)
    {
        if (SaveManager.Instance != null)
        {
            float _newSensitivity = (_change / 10f) + 2.5f;
            character.SetDoubleSwipeSensitivity(_newSensitivity);
            SaveManager.Instance.SetDoubleSwipeSensitivity(_newSensitivity);
        }
    }

    private void UpdateDoubleSwipeToggle(bool _change)
    {
        if (SaveManager.Instance != null)
        {
            character.SetDoubleSwipeStatus(_change);
            SaveManager.Instance.SetDoubleSwipe(_change);
        }
    }
}
