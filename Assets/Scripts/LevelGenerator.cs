using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int frontBlockIndex;

    private int lastBlockIndex;

    private int blocksPassed;
    private float currentBlockSpeed;

    private void Start()
    {
        levelBlockRecycler.RecycleBlock -= GetNewBlock;
        levelBlockRecycler.RecycleBlock += GetNewBlock;

        currentBlockSpeed = startingBlockSpeed;

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
                _newBlock.InitializeBlock();
                _newBlock.SetPosition(Vector3.zero);
                _newBlock.SetSpeed(currentBlockSpeed);
            }
            else
            {
                _newBlock.gameObject.SetActive(true);
                _newBlock.InitializeBlock();
                _newBlock.SetPosition(levelBlocks[i - 1].GetPosition() + (Vector3.forward * levelBlockSize));
                _newBlock.SetSpeed(currentBlockSpeed);
            }
        }
    }

    private void GetNewBlock()
    {
        LevelBlock _newBlock = null;
        if (blocksPassed <= 20)
        {
            _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Easy);
        }
        else if (blocksPassed > 20 && blocksPassed <= 40)
        {
            _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Medium);
        }
        else if (blocksPassed > 40)
        {
            _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Hard);
        }

        _newBlock.gameObject.SetActive(true);
        _newBlock.InitializeBlock();
        _newBlock.SetPosition(levelBlocks[levelBlocks.Count - 1].GetPosition() + (Vector3.forward * levelBlockSize));
        _newBlock.SetSpeed(currentBlockSpeed);
        levelBlocks.Add(_newBlock);
        levelBlocks.RemoveAt(0);
        blocksPassed++;
        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        if (currentBlockSpeed < speedCap)
        {
            currentBlockSpeed += speedIncrease;
           // Debug.Log(currentBlockSpeed);
            for (int i = 0; i < levelBlocks.Count; i++)
            {
                levelBlocks[i].SetSpeed(currentBlockSpeed);
            }
        }
    }

}
