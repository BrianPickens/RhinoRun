using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void GameOver()
    {
        levelGenerator.EndGame();
    }


}
