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
    private GameUI gameUI;

    [SerializeField]
    private ParticleManager particleManager;

    private int points;

    private int distance;

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

        levelGenerator.Initialize(CollectableGained);

        gameUI.Init();



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
    }

    private void GameOver()
    {
        levelGenerator.EndGame();
        gameUI.DisplayEnding(points, distance);
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
                points += 10;
                gameUI.DisplayPoints(points);
                particleManager.CreateGoldParticles(character.MyTransform.position);
                break;

            case CollectableType.Shield:
                Debug.Log("got shield");
                break;

            case CollectableType.Boost:
                Debug.Log("got boost");
                break;

            case CollectableType.MegaCoin:
                Debug.Log("got mega coin");
                break;

            case CollectableType.Stamina:
                //need particles
                character.RestoreChargePower(10f);
                break;

            default:
                Debug.LogError("Unknown Collectable");
                break;
        }

    }

    private void BlockCompleted()
    {
        distance++;
    }

    private void OpenMainMenu()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("MainMenu");
        }
        
    }

    private void OpenUpgrades()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("Upgrades");
        }
    }

    private void Replay()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("PlayScene");
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
