using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameDifficulty { Simple, Easy, Medium, Difficult, Hard }
public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private TutorialManager tutorialManager = null;

    [SerializeField]
    private LevelBlockRecycler levelBlockRecycler = null;

    [SerializeField]
    private List<LevelBlock> levelBlocks = new List<LevelBlock>();

    [SerializeField]
    private LevelBlockPooler levelBlockPooler = null;

    [SerializeField]
    private RhinoDetector rhinoDetection = null;

    [SerializeField]
    private int levelBlockSize = 0;

    [SerializeField]
    private float startingBlockSpeed = 20f;

    [SerializeField]
    private float speedIncrease = 0.1f;

    [SerializeField]
    private float speedCap = 50f;

    [SerializeField]
    private float speedAdjustmentThreshold = 20f;

    [SerializeField]
    private int difficultyThreshold = 15;

    [SerializeField]
    private int minStaminaSpawn = 15;

    [SerializeField]
    private int maxStaminaSpawn = 30;

    [SerializeField]
    private int minPowerUpSpawn = 15;

    [SerializeField]
    private int maxPowerUpSpawn = 30;

    private int currentPowerUpSpawn;

    private int currentStaminaSpawn;

    private GameDifficulty currentDifficulty;
    public GameDifficulty CurrentDifficulty
    {
        get { return currentDifficulty; }
    }

    private int frontBlockIndex;

    private int lastBlockIndex;

    private int staminaSpawnCounter;

    private int powerUpSpawnCounter;

    private int speedThresholdCounter;

    private int blocksPassed;

    private int totalBlocksPassed;

    private float currentBlockSpeed;

    private int numBlocksGenerated;

    private List<Upgrades> avaliableUpgrades = new List<Upgrades>();

    private int currentPowerUpDropLevel;

    private float coinSpinTime;

    private bool tutorialCompleted;

    private int tutorialCount;

    private Action<CollectableType> CollectableCallback;

    public Action OnBlockPassed;

    public void Initialize(Action<CollectableType> _collectableCallback, List<Upgrades> _avaliableUpgrades, bool _tutorialCompleted)
    {

        avaliableUpgrades = _avaliableUpgrades;

        levelBlockRecycler.RecycleBlock -= GetNewBlock;
        levelBlockRecycler.RecycleBlock += GetNewBlock;

        CollectableCallback = _collectableCallback;

        tutorialCompleted = _tutorialCompleted;

        currentDifficulty = GameDifficulty.Simple;

        currentPowerUpDropLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.PowerUpDropUpgrade);

        SetPowerUpSpawnGates();

        currentStaminaSpawn = GenerateStaminaSpawnNumber();
        currentPowerUpSpawn = GeneratePowerUpSpawnNumber();

        currentBlockSpeed = startingBlockSpeed;

        if (!tutorialCompleted)
        {
            GenerateTutorialLevel();
        }
        else
        {
            GenerateLevel();
        }
    }

    private void Update()
    {
        coinSpinTime += Time.deltaTime;
        if (coinSpinTime > 1f)
        {
            coinSpinTime = 0f;
        }
    }

    public void GenerateTutorialLevel()
    {
        for (int i = 0; i < 10; i++)
        {
            LevelBlock _newBlock = null;
            if (i < 3)
            {
                _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.None);
            }
            else
            {
                _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Tutorial);
            }
            levelBlocks.Add(_newBlock);
            if (i == 0)
            {
                _newBlock.gameObject.SetActive(true);
                _newBlock.InitializeBlock(CollectableCallback, numBlocksGenerated, coinSpinTime);
                _newBlock.SetPosition(Vector3.zero);
                _newBlock.SetSpeed(currentBlockSpeed);
                numBlocksGenerated++;

            }
            else
            {
                _newBlock.gameObject.SetActive(true);
                _newBlock.InitializeBlock(CollectableCallback, numBlocksGenerated, coinSpinTime);
                _newBlock.SetPosition(levelBlocks[i - 1].GetPosition() + (Vector3.forward * levelBlockSize));
                _newBlock.SetSpeed(currentBlockSpeed);
                numBlocksGenerated++;
            }
        }

        rhinoDetection.SetSpeed(currentBlockSpeed);
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < 10; i++)
        {
            LevelBlock _newBlock = null;
            if (i < 3)
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
                _newBlock.InitializeBlock(CollectableCallback, numBlocksGenerated, coinSpinTime);
                _newBlock.SetPosition(Vector3.zero);
                _newBlock.SetSpeed(currentBlockSpeed);
                numBlocksGenerated++;

            }
            else
            {
                _newBlock.gameObject.SetActive(true);
                _newBlock.InitializeBlock(CollectableCallback, numBlocksGenerated, coinSpinTime);
                _newBlock.SetPosition(levelBlocks[i - 1].GetPosition() + (Vector3.forward * levelBlockSize));
                _newBlock.SetSpeed(currentBlockSpeed);
                numBlocksGenerated++;
            }
        }

        rhinoDetection.SetSpeed(currentBlockSpeed);

    }

    private void GetNewBlock()
    {

        if (!tutorialCompleted)
        {
            tutorialCount++;

            if (tutorialCount == 2)
            {
                tutorialManager.DisplayArrowTutorial();
            }
            else if (tutorialCount == 7)
            {
                tutorialManager.DisplayChargeTutorial();
            }
            else if (tutorialCount == 8)
            {
                tutorialManager.DisplayStaminaTutorial();
            }
            else if (tutorialCount == 10)
            {
                tutorialManager.SetTutorialCompleted();
                tutorialCompleted = true;
            }
        }

        LevelBlock _newBlock = null;
        if (staminaSpawnCounter >= currentStaminaSpawn)
        {
            _newBlock = levelBlockPooler.GetLevelBlock(BlockDifficulty.Stamina);
            staminaSpawnCounter = 0;
            currentStaminaSpawn = GenerateStaminaSpawnNumber();
        }
        else if (powerUpSpawnCounter >= currentPowerUpSpawn)
        {
            _newBlock = levelBlockPooler.GetPowerUpBlock(avaliableUpgrades);
            powerUpSpawnCounter = 0;
            currentPowerUpSpawn = GeneratePowerUpSpawnNumber();
        }
        else
        {
            _newBlock = DetermineBlock(currentDifficulty);
        }

        _newBlock.gameObject.SetActive(true);
        _newBlock.InitializeBlock(CollectableCallback, numBlocksGenerated, coinSpinTime);
        _newBlock.SetPosition(levelBlocks[levelBlocks.Count - 1].GetPosition() + (Vector3.forward * levelBlockSize));
        _newBlock.SetSpeed(currentBlockSpeed);
        levelBlocks.Add(_newBlock);
        levelBlocks.RemoveAt(0);
        numBlocksGenerated++;
        blocksPassed++;
        totalBlocksPassed++;
        staminaSpawnCounter++;
        powerUpSpawnCounter++;

        if (blocksPassed >= difficultyThreshold)
        {
            blocksPassed = 0;
            IncreaseDifficulty();
        }

        if (OnBlockPassed != null)
        {
            OnBlockPassed();
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
                gate1 = 100;
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
        speedThresholdCounter++;
        if (speedThresholdCounter >= speedAdjustmentThreshold)
        {
            speedThresholdCounter = 0;
            speedIncrease -= 0.1f;
            if (speedIncrease < 0.1f)
            {
                speedIncrease = 0.1f;
            }
        }

        if (currentBlockSpeed < speedCap)
        {
            currentBlockSpeed += speedIncrease;
            //Debug.Log(currentBlockSpeed);
            for (int i = 0; i < levelBlocks.Count; i++)
            {
                levelBlocks[i].SetSpeed(currentBlockSpeed);
            }
            rhinoDetection.SetSpeed(currentBlockSpeed);
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

    private int GenerateStaminaSpawnNumber()
    {
        return UnityEngine.Random.Range(minStaminaSpawn, maxStaminaSpawn);
    }

    private int GeneratePowerUpSpawnNumber()
    {
        return UnityEngine.Random.Range(minPowerUpSpawn, maxPowerUpSpawn);
    }

    private void SetPowerUpSpawnGates()
    {
        switch (currentPowerUpDropLevel)
        {
            case 0:
                minPowerUpSpawn = 20;
                maxPowerUpSpawn = 30;
                break;

            case 1:
                minPowerUpSpawn = 18;
                maxPowerUpSpawn = 28;
                break;

            case 2:
                minPowerUpSpawn = 16;
                maxPowerUpSpawn = 26;
                break;

            case 3:
                minPowerUpSpawn = 14;
                maxPowerUpSpawn = 24;
                break;

            case 4:
                minPowerUpSpawn = 12;
                maxPowerUpSpawn = 22;
                break;
            default:
                Debug.LogError("Invalid power up level");
                break;
        }
    }

    public void EndGame()
    {
        for (int i = 0; i < levelBlocks.Count; i++)
        {
            levelBlocks[i].SetSpeed(0);
        }
        //gameOver = true;
       // StartCoroutine(EndGameRoutine());

    }

    //IEnumerator EndGameRoutine()
    //{
    //    //necessary because movement and detection of collision are happening in the same fixed update frame, so we need to wait for the end
    //    //the fixed update frame to stop all the levelblocks
    //    yield return new WaitForEndOfFrame();
    //    for (int i = 0; i < levelBlocks.Count; i++)
    //    {
    //        levelBlocks[i].SetSpeed(0);
    //    }
    //}

}
