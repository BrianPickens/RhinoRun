using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameDifficulty { Simple, Easy, Medium, Difficult, Hard }
public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private LevelBlockRecycler levelBlockRecycler;

    [SerializeField]
    private List<LevelBlock> levelBlocks;

    [SerializeField]
    private LevelBlockPooler levelBlockPooler;

    [SerializeField]
    private int levelBlockSize;

    [SerializeField]
    private float startingBlockSpeed = 20f;

    [SerializeField]
    private float speedIncrease = 0.1f;

    [SerializeField]
    private float speedCap = 50f;

    [SerializeField]
    private int difficultyThreshold = 15;

    private GameDifficulty currentDifficulty;
    public GameDifficulty CurrentDifficulty
    {
        get { return currentDifficulty; }
    }

    private int frontBlockIndex;

    private int lastBlockIndex;

    private int blocksPassed;
    private float currentBlockSpeed;

    private Action<CollectableType> CollectableCallback;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EndGame();
        }
    }

    public void Initialize(Action<CollectableType> _collectableCallback)
    {
        levelBlockRecycler.RecycleBlock -= GetNewBlock;
        levelBlockRecycler.RecycleBlock += GetNewBlock;

        CollectableCallback = _collectableCallback;

        currentDifficulty = GameDifficulty.Simple;

        currentBlockSpeed = startingBlockSpeed;
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < 10; i++)
        {
            LevelBlock _newBlock = null;
            if (i < 5)
            {
                _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.None);
            }
            else
            {
                _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Easy);
            }
            levelBlocks.Add(_newBlock);
            if (i == 0)
            {
                _newBlock.gameObject.SetActive(true);
                _newBlock.InitializeBlock(CollectableCallback);
                _newBlock.SetPosition(Vector3.zero);
                _newBlock.SetSpeed(currentBlockSpeed);
            }
            else
            {
                _newBlock.gameObject.SetActive(true);
                _newBlock.InitializeBlock(CollectableCallback);
                _newBlock.SetPosition(levelBlocks[i - 1].GetPosition() + (Vector3.forward * levelBlockSize));
                _newBlock.SetSpeed(currentBlockSpeed);
            }
        }
    }

    private void GetNewBlock()
    {
        LevelBlock _newBlock = null;
        _newBlock = DetermineBlock(currentDifficulty);

        _newBlock.gameObject.SetActive(true);
        _newBlock.InitializeBlock(CollectableCallback);
        _newBlock.SetPosition(levelBlocks[levelBlocks.Count - 1].GetPosition() + (Vector3.forward * levelBlockSize));
        _newBlock.SetSpeed(currentBlockSpeed);
        levelBlocks.Add(_newBlock);
        levelBlocks.RemoveAt(0);
        blocksPassed++;
        if (blocksPassed >= difficultyThreshold)
        {
            blocksPassed = 0;
            IncreaseDifficulty();
        }
        IncreaseSpeed();
    }

    private LevelBlock DetermineBlock(GameDifficulty _currentDifficulty)
    {
        LevelBlock newBlock = null;
        int gate1 = 0;
        int gate2 = 0;
        int chance = 0;

        switch (_currentDifficulty)
        {
            case GameDifficulty.Simple:
                gate1 = 70;
                gate2 = 100;
                break;

            case GameDifficulty.Easy:
                gate1 = 50;
                gate2 = 100;
                break;

            case GameDifficulty.Medium:
                gate1 = 40;
                gate2 = 80;
                break;

            case GameDifficulty.Difficult:
                gate1 = 20;
                gate2 = 60;
                break;

            case GameDifficulty.Hard:
                gate1 = 20;
                gate2 = 40;
                break;

            default:
                gate1 = 70;
                gate2 = 100;
                break;
        }

        chance = UnityEngine.Random.Range(0, 100);

        if (chance <= gate1)
        {
            newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Easy);
        }
        else if (chance > gate1 && chance <= gate2)
        {
            newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Medium);
        }
        else if (chance > gate2)
        {
            newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Hard);
        }

        return newBlock;

    }

    private void IncreaseSpeed()
    {
        if (currentBlockSpeed < speedCap)
        {
            currentBlockSpeed += speedIncrease;
            //Debug.Log(currentBlockSpeed);
            for (int i = 0; i < levelBlocks.Count; i++)
            {
                levelBlocks[i].SetSpeed(currentBlockSpeed);
            }
        }
    }

    private void IncreaseDifficulty()
    {
        int difficultyCap = Enum.GetValues(typeof(GameDifficulty)).Length;
        if ((int)currentDifficulty + 1 < difficultyCap)
        {
            currentDifficulty += 1;
        }

    }

    public void EndGame()
    {

        StartCoroutine(EndGameRoutine());

    }

    IEnumerator EndGameRoutine()
    {
        //necessary because movement and detection of collision are happening in the same fixed update frame, so we need to wait for the end
        //the fixed update frame to stop all the levelblocks
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < levelBlocks.Count; i++)
        {
            levelBlocks[i].SetSpeed(0);
        }
    }

}
