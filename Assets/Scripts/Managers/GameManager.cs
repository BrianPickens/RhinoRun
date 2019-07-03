using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { WaitingForStart, Playing, GameOver }
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Character character = null;

    [SerializeField]
    private LevelGenerator levelGenerator = null;

    [SerializeField]
    private PowerUpsManager powerUpsManager = null;

    [SerializeField]
    private GameUI gameUI = null;

    [SerializeField]
    private ParticleManager particleManager = null;

    [SerializeField]
    private AudioClip coinCollectLowSound = null;

    [SerializeField]
    private AudioClip coinCollectMidSound = null;

    [SerializeField]
    private AudioClip coinCollectHighSound = null;

    [SerializeField]
    private AudioClip powerUpSound = null;

    [SerializeField]
    private AudioClip staminaSound = null;

    private int currentCoinUpgradeLevel;

    private int currentMegaCoinUpgradeLevel;

    private int points;

    private int distance;

    private bool watchedRewardedAd;

    private string currentLoadString;

    private int coinSoundLevel;

    private bool tutorialCompleted;

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

        tutorialCompleted = false;
        if (SaveManager.Instance != null)
        {
            tutorialCompleted = SaveManager.Instance.GetTutorialStatus();
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

        gameUI.OnTutorialChange = null;
        gameUI.OnTutorialChange += UpdateTutorialStatus;

        gameUI.OnRewardedAdConfirmation = null;
        gameUI.OnRewardedAdConfirmation += CheckForRewardedAd;

        gameUI.OnGameCenterPress = null;
        gameUI.OnGameCenterPress += InitializeGameCenter;

        levelGenerator.OnBlockPassed = null;
        levelGenerator.OnBlockPassed += BlockCompleted;

        powerUpsManager.Initialize();

        levelGenerator.Initialize(CollectableGained, powerUpsManager.GetAvaliablePowerUps(), tutorialCompleted);

        gameUI.Init();

        currentCoinUpgradeLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.CoinsUpgrade);
        currentMegaCoinUpgradeLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.MegaCoinUpgrade);

        bool musicOn = true;
        bool soundEffectsOn = true;
        if (SoundManager.Instance != null)
        {
            musicOn = SoundManager.Instance.MusicOn;
            soundEffectsOn = SoundManager.Instance.SoundEffectsOn;
        }
        gameUI.InitializeSoundPreferences(musicOn, soundEffectsOn);

        gameUI.InitializeTutorialStatus(tutorialCompleted);

        if (!tutorialCompleted)
        {
            gameUI.HideControls();
        }

        character.Initialize();

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
                if (coinSoundLevel == 0)
                {
                    PlayCoinSound(coinCollectLowSound);
                    coinSoundLevel++;
                }
                else if (coinSoundLevel == 1)
                {
                    PlayCoinSound(coinCollectMidSound);
                    coinSoundLevel++;
                }
                else if (coinSoundLevel == 2)
                {
                    PlayCoinSound(coinCollectHighSound);
                }
                CoinCollected();
                break;

            case CollectableType.Charge:
                // Debug.Log("got Charge");
                PlaySound(powerUpSound);
                particleManager.CreateParticles(ParticleType.UnlimitedCharge, character.MyTransform.position);
                powerUpsManager.ActivatePowerUp(_collectableType);
                break;

            case CollectableType.Shield:
                // Debug.Log("got shield");
                PlaySound(powerUpSound);
                particleManager.CreateParticles(ParticleType.Shield, character.MyTransform.position);
                powerUpsManager.ActivatePowerUp(_collectableType);
                break;

            case CollectableType.MegaCoin:
                // Debug.Log("got mega coin");
                PlaySound(powerUpSound);
                MegaCoinCollected();
                particleManager.CreateParticles(ParticleType.Diamond, character.MyTransform.position);
                break;

            case CollectableType.Stamina:
                // Debug.Log("got stamina");
                PlaySound(staminaSound);
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
        coinSoundLevel = 0;
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
        bool hasRemoveAds = false;
        if (SaveManager.Instance != null)
        {
            hasRemoveAds = SaveManager.Instance.GetHasRemoveAdsStatus();
        }

        int numPlays = StaticInfo.GetNumberOfPlays();
        if (numPlays > 0 && numPlays % 3 == 0 && !hasRemoveAds && !watchedRewardedAd)
        {
            if (UnityAds.CheckForAd())
            {
                SoundManager.Instance.FadeOutBackgroundMusic(1f);
                UnityAds.ShowAd(AdCompleted);
            }
            else
            {
               // Debug.Log("Add didn't show");
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
            SoundManager.Instance.FadeOutBackgroundMusic(1f);
            UnityAds.ShowRewardedAd(AdCompleted);
        }
        else
        {
            gameUI.DisplayRewardedAdResult("No Ads Avaliable", false);
        }
    }

    private void AdCompleted(AdType _adType, bool _completed)
    {
        switch (_adType)
        {
            case AdType.Normal:
                if (_completed)
                {
                    SoundManager.Instance.FadeInBackgroundMusic(1f);
                  //  Debug.Log("normal ad completed");
                }
                else
                {
                    SoundManager.Instance.FadeInBackgroundMusic(1f);
                   // Debug.Log("normal ad failed");
                }

                LoadScene(currentLoadString);

                break;

            case AdType.Rewarded:

                if (_completed)
                {
                    if (SaveManager.Instance != null)
                    {
                        SaveManager.Instance.UpdateCoins(points);
                    }
                    SoundManager.Instance.FadeInBackgroundMusic(1f);
                    gameUI.DisableMultiplierButton();
                    string pointsString = points.ToString("#,#");
                    string resultString = "You gained a bonus " + pointsString + " coins!";
                    gameUI.DisplayRewardedAdResult(resultString, true);
                   // Debug.Log("rewarded ad completed");
                    watchedRewardedAd = true;
                }
                else
                {
                    SoundManager.Instance.FadeInBackgroundMusic(1f);
                    gameUI.DisplayRewardedAdResult("Rewarded Ad Failed", false);
                   // Debug.Log("rewarded ad failed or skipped");
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
            SceneLoadingManager.Instance.LoadSceneAsync(_scene);
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

    private void UpdateTutorialStatus(bool _tutorialCompleted)
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SetTutorialStatus(_tutorialCompleted);
        }
    }

    private void InitializeGameCenter()
    {
        GameCenter.AuthenticateUser();
    }

    private void PlaySound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
        }
    }

    private void PlayCoinSound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundNoPitchShift(_clip, 0.25f);
        }
    }

}
