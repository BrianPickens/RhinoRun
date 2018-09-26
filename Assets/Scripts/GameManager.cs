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

    private void Start()
    {
        character.OnGameOver += GameOver;

        levelGenerator.Initialize(CollectableGained);
        character.Initialize();

    }

    private void GameOver()
    {
        levelGenerator.EndGame();
    }

    private void CollectableGained(CollectableType _collectableType)
    {
        switch (_collectableType)
        {
            case CollectableType.Gold:
                gameUI.AddPoints(10);
                particleManager.CreateGoldParticles(character.MyTransform.position);
                break;

            case CollectableType.PowerUp:

                break;

            case CollectableType.Stamina:
                //need particles
                character.RestoreChargePower(25f);
                break;

            default:
                Debug.LogError("Unknown Collectable");
                break;
        }

    }

    public void ResetGame()
    {
        SceneManager.LoadScene("PlayScene");
    }



}
