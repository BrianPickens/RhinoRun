using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockDifficulty { None, Easy, Medium, Hard, MegaCoin, Stamina, Boost, UnlimitedCharge }
public class LevelBlockPooler : MonoBehaviour {

    [SerializeField]
    private List<LevelBlock> EmptyBlocks;

    [SerializeField]
    private List<LevelBlock> EasyBlocks;

    [SerializeField]
    private List<LevelBlock> MediumBlocks;

    [SerializeField]
    private List<LevelBlock> HardBlocks;

    [SerializeField]
    private List<LevelBlock> MegaCoinBlocks;

    [SerializeField]
    private List<LevelBlock> BoostBlocks;

    [SerializeField]
    private List<LevelBlock> UnlimitedChargeBlocks;

    [SerializeField]
    private List<LevelBlock> StaminaBlocks;

    public void Start()
    {
        for (int i = 0; i < EmptyBlocks.Count; i++)
        {
            EmptyBlocks[i].BlockRecycled -= AddBlockToList;
            EmptyBlocks[i].BlockRecycled += AddBlockToList;
        }

        for (int i = 0; i < EasyBlocks.Count; i++)
        {
            EasyBlocks[i].BlockRecycled -= AddBlockToList;
            EasyBlocks[i].BlockRecycled += AddBlockToList;
        }

        for (int i = 0; i < MediumBlocks.Count; i++)
        {
            MediumBlocks[i].BlockRecycled -= AddBlockToList;
            MediumBlocks[i].BlockRecycled += AddBlockToList;
        }

        for (int i = 0; i < HardBlocks.Count; i++)
        {
            HardBlocks[i].BlockRecycled -= AddBlockToList;
            HardBlocks[i].BlockRecycled += AddBlockToList;
        }

        for (int i = 0; i < BoostBlocks.Count; i++)
        {
            BoostBlocks[i].BlockRecycled -= AddBlockToList;
            BoostBlocks[i].BlockRecycled += AddBlockToList;
        }

        for (int i = 0; i < MegaCoinBlocks.Count; i++)
        {
            MegaCoinBlocks[i].BlockRecycled -= AddBlockToList;
            MegaCoinBlocks[i].BlockRecycled += AddBlockToList;
        }

        for (int i = 0; i < UnlimitedChargeBlocks.Count; i++)
        {
            UnlimitedChargeBlocks[i].BlockRecycled -= AddBlockToList;
            UnlimitedChargeBlocks[i].BlockRecycled += AddBlockToList;
        }

        for (int i = 0; i < StaminaBlocks.Count; i++)
        {
            StaminaBlocks[i].BlockRecycled -= AddBlockToList;
            StaminaBlocks[i].BlockRecycled += AddBlockToList;
        }
    }

    public LevelBlock GetLevelBlock(BlockDifficulty _blockDifficulty)
    {
        LevelBlock _newBlock = null;
        int _randomIndex = 0;
        switch (_blockDifficulty)
        {
            case BlockDifficulty.None:
                _randomIndex = Random.Range(0, EmptyBlocks.Count);
                _newBlock = EmptyBlocks[_randomIndex];
                EmptyBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Easy:
                _randomIndex = Random.Range(0, EasyBlocks.Count);
                _newBlock = EasyBlocks[_randomIndex];
                EasyBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Medium:
                _randomIndex = Random.Range(0, MediumBlocks.Count);
                _newBlock = MediumBlocks[_randomIndex];
                MediumBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Hard:
                _randomIndex = Random.Range(0, HardBlocks.Count);
                _newBlock = HardBlocks[_randomIndex];
                HardBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Boost:
                _randomIndex = Random.Range(0, BoostBlocks.Count);
                _newBlock = BoostBlocks[_randomIndex];
                BoostBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.MegaCoin:
                _randomIndex = Random.Range(0, MegaCoinBlocks.Count);
                _newBlock = MegaCoinBlocks[_randomIndex];
                MegaCoinBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.UnlimitedCharge:
                _randomIndex = Random.Range(0, UnlimitedChargeBlocks.Count);
                _newBlock = UnlimitedChargeBlocks[_randomIndex];
                UnlimitedChargeBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Stamina:
                _randomIndex = Random.Range(0, StaminaBlocks.Count);
                _newBlock = StaminaBlocks[_randomIndex];
                StaminaBlocks.Remove(_newBlock);
                break;

            default:
                _randomIndex = Random.Range(0, EmptyBlocks.Count);
                _newBlock = EmptyBlocks[_randomIndex];
                EmptyBlocks.Remove(_newBlock);
                Debug.LogError("Unknown Block Difficulty");
                break;
        }

        return _newBlock;


    }

    public LevelBlock GetPowerUpBlock()
    {
        LevelBlock _newBlock = null;
        int _randomIndex = 0;

        BlockDifficulty blockDifficulty = DeterminePowerUp();
        switch (blockDifficulty)
        {
            case BlockDifficulty.Boost:
                _randomIndex = Random.Range(0, BoostBlocks.Count);
                _newBlock = BoostBlocks[_randomIndex];
                BoostBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.MegaCoin:
                _randomIndex = Random.Range(0, MegaCoinBlocks.Count);
                _newBlock = MegaCoinBlocks[_randomIndex];
                MegaCoinBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.UnlimitedCharge:
                _randomIndex = Random.Range(0, UnlimitedChargeBlocks.Count);
                _newBlock = UnlimitedChargeBlocks[_randomIndex];
                UnlimitedChargeBlocks.Remove(_newBlock);
                break;

            default:
                _randomIndex = Random.Range(0, MegaCoinBlocks.Count);
                _newBlock = MegaCoinBlocks[_randomIndex];
                MegaCoinBlocks.Remove(_newBlock);
                break;
        }

        return _newBlock;

    }

    private BlockDifficulty DeterminePowerUp()
    {
        Debug.Log("STILL HAVE WORK TO DO HERE");
        BlockDifficulty blocktype = BlockDifficulty.MegaCoin;


        return blocktype;
    }

    public void AddBlockToList(BlockDifficulty _blockDifficulty, LevelBlock _levelBlock)
    {
        switch (_blockDifficulty)
        {
            case BlockDifficulty.None:
                EmptyBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Easy:
                EasyBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Medium:
                MediumBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Hard:
                HardBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Boost:
                BoostBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.MegaCoin:
                MegaCoinBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.UnlimitedCharge:
                UnlimitedChargeBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Stamina:
                StaminaBlocks.Add(_levelBlock);
                break;

            default:
                Debug.LogError("Unknown Block Difficulty");
                break;
        }
    }


}
