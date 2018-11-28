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

        gameUI.Init();

        character.OnGameOver += GameOver;

        gameUI.OnMenuPress = null;
        gameUI.OnMenuPress += OpenMainMenu;

        gameUI.OnUpgradesPress = null;
        gameUI.OnUpgradesPress += OpenUpgrades;

        gameUI.OnReplayPress = null;
        gameUI.OnReplayPress += Replay;

        gameUI.DisplayPoints(0);
        gameUI.DisplayDistance(0);

        levelGenerator.OnBlockPassed = null;
        levelGenerator.OnBlockPassed += BlockCompleted;

        levelGenerator.Initialize(CollectableGained);
        character.Initialize();
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

            case CollectableType.PowerUp:

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
        gameUI.DisplayDistance(distance);
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
}
