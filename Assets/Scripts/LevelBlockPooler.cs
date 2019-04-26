using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockDifficulty { None, Easy, Medium, Hard, MegaCoin, Stamina, Shield, Charge, Tutorial }
public class LevelBlockPooler : MonoBehaviour {

    [SerializeField]
    private List<LevelBlock> EmptyBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> EasyBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> MediumBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> HardBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> MegaCoinBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> ShieldBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> UnlimitedChargeBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> StaminaBlocks = new List<LevelBlock>();

    [SerializeField]
    private List<LevelBlock> TutorialBlocks = new List<LevelBlock>();

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

        for (int i = 0; i < ShieldBlocks.Count; i++)
        {
            ShieldBlocks[i].BlockRecycled -= AddBlockToList;
            ShieldBlocks[i].BlockRecycled += AddBlockToList;
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

        for (int i = 0; i < TutorialBlocks.Count; i++)
        {
            TutorialBlocks[i].BlockRecycled -= AddBlockToList;
            TutorialBlocks[i].BlockRecycled += AddBlockToList;
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

            case BlockDifficulty.Shield:
                _randomIndex = Random.Range(0, ShieldBlocks.Count);
                _newBlock = ShieldBlocks[_randomIndex];
                ShieldBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.MegaCoin:
                _randomIndex = Random.Range(0, MegaCoinBlocks.Count);
                _newBlock = MegaCoinBlocks[_randomIndex];
                MegaCoinBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Charge:
                _randomIndex = Random.Range(0, UnlimitedChargeBlocks.Count);
                _newBlock = UnlimitedChargeBlocks[_randomIndex];
                UnlimitedChargeBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Stamina:
                _randomIndex = Random.Range(0, StaminaBlocks.Count);
                _newBlock = StaminaBlocks[_randomIndex];
                StaminaBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Tutorial:
                _newBlock = TutorialBlocks[0];
                TutorialBlocks.RemoveAt(0);
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

    public LevelBlock GetPowerUpBlock(List<Upgrades> _avaliableUpgrades)
    {
        LevelBlock _newBlock = null;

        int _randomUpgradeIndex = Random.Range(0, _avaliableUpgrades.Count);

        Upgrades _currentUpgrade = _avaliableUpgrades[_randomUpgradeIndex];

        BlockDifficulty blockDifficulty = DeterminePowerUpBlock(_currentUpgrade);

        int _randomBlockIndex = 0;
        switch (blockDifficulty)
        {
            case BlockDifficulty.Shield:
                _randomBlockIndex = Random.Range(0, ShieldBlocks.Count);
                _newBlock = ShieldBlocks[_randomBlockIndex];
                ShieldBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.MegaCoin:
                _randomBlockIndex = Random.Range(0, MegaCoinBlocks.Count);
                _newBlock = MegaCoinBlocks[_randomBlockIndex];
                MegaCoinBlocks.Remove(_newBlock);
                break;

            case BlockDifficulty.Charge:
                _randomBlockIndex = Random.Range(0, UnlimitedChargeBlocks.Count);
                _newBlock = UnlimitedChargeBlocks[_randomBlockIndex];
                UnlimitedChargeBlocks.Remove(_newBlock);
                break;

            default:
                _randomBlockIndex = Random.Range(0, ShieldBlocks.Count);
                _newBlock = ShieldBlocks[_randomBlockIndex];
                ShieldBlocks.Remove(_newBlock);
                break;
        }

        return _newBlock;

    }

    private BlockDifficulty DeterminePowerUpBlock(Upgrades _upgradeType)
    {
        BlockDifficulty blockDifficulty = BlockDifficulty.None;

        switch (_upgradeType)
        {
            case Upgrades.ChargeUpgrade:
                blockDifficulty = BlockDifficulty.Charge;
                break;

            case Upgrades.ShieldUpgrade:
                blockDifficulty = BlockDifficulty.Shield;
                break;

            case Upgrades.MegaCoinUpgrade:
                blockDifficulty = BlockDifficulty.MegaCoin;
                break;

            default:
                blockDifficulty = BlockDifficulty.Shield;
                break;
        }

        return blockDifficulty;

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

            case BlockDifficulty.Shield:
                ShieldBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.MegaCoin:
                MegaCoinBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Charge:
                UnlimitedChargeBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Stamina:
                StaminaBlocks.Add(_levelBlock);
                break;

            case BlockDifficulty.Tutorial:
                TutorialBlocks.Add(_levelBlock);
                break;
            default:
                Debug.LogError("Unknown Block Difficulty");
                break;
        }
    }


}
