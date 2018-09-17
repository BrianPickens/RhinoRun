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
        Debug.Log(_collectableType);
        Debug.Log("this happened");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("PlayScene");
    }



}
