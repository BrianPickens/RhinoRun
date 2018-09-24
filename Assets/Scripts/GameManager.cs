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

        levelGenerator.Initialize(GoldCollected);
        character.Initialize();

    }

    private void GameOver()
    {
        levelGenerator.EndGame();
    }

    private void GoldCollected(CollectableType _collectableType)
    {
        gameUI.AddPoints(10);
        particleManager.CreateGoldParticles(character.MyTransform.position);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("PlayScene");
    }



}
