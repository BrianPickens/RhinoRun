using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerSave
{
    public int coins;
    public int highscore;
    public int coinsUpgradeLevel;
    public int staminaUpgradeLevel;
    public int chargeUpgradeLevel;
    public int shieldUpgradeLevel;
    public int megaCoinUpgradeLevel;
    public int powerUpDropUpgradeLevel;

    public const string coinsString = "coins";
    public const string highscoreString = "highscore";
    public const string coinUpgradeString = "coinUpgrade";
    public const string staminaUpgradeString = "staminaUpgrade";
    public const string chargeUpgradeString = "chargeUpgrade";
    public const string shieldUpgradeString = "shieldUpgrade";
    public const string megaCoinUpgradeString = "megaCoinUpgrade";
    public const string powerUpDropUpgradeString = "powerUpDropUpgrade";

    private List<string> saveDataList = new List<string>();

    public PlayerSave(int _coins = 0, int _highscore = 0, int _coinsUpgradeLevel = 0, int _staminaUpgradeLevel = 0, int _chargeUpgradeLevel = 0, int _shieldUpgradeLevel = 1, int _megaCoinUpgradeLevel = 0, int _powerUpDropUpgradeLevel = 0)
    {
        coins = _coins;
        highscore = _highscore;
        coinsUpgradeLevel = _coinsUpgradeLevel;
        staminaUpgradeLevel = _staminaUpgradeLevel;
        chargeUpgradeLevel = _chargeUpgradeLevel;
        shieldUpgradeLevel = _shieldUpgradeLevel;
        megaCoinUpgradeLevel = _megaCoinUpgradeLevel;
        powerUpDropUpgradeLevel = _powerUpDropUpgradeLevel;
    }

    public string GetSavableData()
    {
        //list is formated, string name followed by string level
        List<string> tempSaveList = new List<string>();

        tempSaveList.Add(coinsString);
        tempSaveList.Add(Utility.IntToString(coins));

        tempSaveList.Add(highscoreString);
        tempSaveList.Add(Utility.IntToString(highscore));

        tempSaveList.Add(coinUpgradeString);
        tempSaveList.Add(Utility.IntToString(coinsUpgradeLevel));

        tempSaveList.Add(staminaUpgradeString);
        tempSaveList.Add(Utility.IntToString(staminaUpgradeLevel));

        tempSaveList.Add(chargeUpgradeString);
        tempSaveList.Add(Utility.IntToString(chargeUpgradeLevel));

        tempSaveList.Add(shieldUpgradeString);
        tempSaveList.Add(Utility.IntToString(shieldUpgradeLevel));

        tempSaveList.Add(megaCoinUpgradeString);
        tempSaveList.Add(Utility.IntToString(megaCoinUpgradeLevel));

        tempSaveList.Add(powerUpDropUpgradeString);
        tempSaveList.Add(Utility.IntToString(powerUpDropUpgradeLevel));

        string saveData = Utility.ConvertStringListToString(tempSaveList);

        return saveData;
    }

    //get player levels from teh save string
    public void ConvertDataFromString(string _saveData)
    {
        saveDataList = Utility.ConvertStringToStringList(_saveData);
        coins = GetSavedIntByString(coinsString);
        highscore = GetSavedIntByString(highscoreString);
        coinsUpgradeLevel = GetSavedIntByString(coinUpgradeString);
        staminaUpgradeLevel = GetSavedIntByString(staminaUpgradeString);
        chargeUpgradeLevel = GetSavedIntByString(chargeUpgradeString);
        shieldUpgradeLevel = GetSavedIntByString(shieldUpgradeString);
        megaCoinUpgradeLevel = GetSavedIntByString(megaCoinUpgradeString);
        powerUpDropUpgradeLevel = GetSavedIntByString(powerUpDropUpgradeString);

    }

    //pass in a string, and get and int level returned
    private int GetSavedIntByString(string _stringId)
    {
        for (int i = 0; i < saveDataList.Count; i++)
        {
            if (_stringId == saveDataList[i])
            {
                return Utility.StringToInt(saveDataList[i + 1]);
            }
        }

        return 0;
    }

}
//PLAYER SAVE ENDS HERE


//SAVE MANAGER STARTS HERE
public enum Upgrades { StaminaUpgrade, CoinsUpgrade, ChargeUpgrade, ShieldUpgrade, MegaCoinUpgrade, PowerUpDropUpgrade }
public class SaveManager : MonoBehaviour
{

    private static SaveManager instance = null;
    public static SaveManager Instance
    {
        get { return instance; }
    }

    private PlayerSave playerSave;

    public const string saveString = "gameSaveData";

    private float swipeSensitivity;

    private float doubleSwipeSensitivity;

    private bool doubleSwipeOn;

    private const string swipeSensitivityString = "swipeSensitivity";

    private const string doubleSwipeSensitivityString = "doubleSwipeSensitivity";

    private const string doubleSwipeString = "doubleSwipe";

    private bool initialized;
    public bool Initialized
    {
        get { return initialized; }
    }

    public void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        GetPlayerData();
        initialized = true;

    }

    private void GetPlayerData()
    {
        //create default save data
        playerSave = new PlayerSave();

        PlayerSave cloudSave = LoadCloudData();
        PlayerSave localSave = LoadLocalData();
        PlayerSave compiledSave = CompareCloudAndLocalData(cloudSave, localSave);

        playerSave = compiledSave;
        SaveData();
    }

    private PlayerSave LoadCloudData()
    {
        CloudSaving.GatherCloudData();//need to finish how gathering cloud data works
        //creat default save data
        PlayerSave cloudSave = new PlayerSave();
        //get save data string
        string cloudSaveData = CloudSaving.GetCloudString(saveString);
        //convert the data
        cloudSave.ConvertDataFromString(cloudSaveData);

        return cloudSave;
    }

    private PlayerSave LoadLocalData()
    {
        PlayerSave localSave = new PlayerSave();

        string localSaveData = LocalSaving.GetLocalString(saveString);

        localSave.ConvertDataFromString(localSaveData);

        //only saved local
        swipeSensitivity = LocalSaving.GetLocalFloat(swipeSensitivityString, 0.2f);
        doubleSwipeSensitivity = LocalSaving.GetLocalFloat(doubleSwipeSensitivityString, 3f);
        doubleSwipeOn = LocalSaving.GetLocalBool(doubleSwipeString, 1);
        //end only saved local

        return localSave;
    }

    //compare local and cloud save and give player the best from both
    private PlayerSave CompareCloudAndLocalData(PlayerSave _cloudSave, PlayerSave _localSave)
    {
        PlayerSave compiledSave = new PlayerSave();
        compiledSave.coins = Mathf.Max(_cloudSave.coins, _localSave.coins);
        compiledSave.highscore = Mathf.Max(_cloudSave.highscore, _localSave.highscore);
        compiledSave.coinsUpgradeLevel = Mathf.Max(_cloudSave.coinsUpgradeLevel, _localSave.coinsUpgradeLevel);
        compiledSave.staminaUpgradeLevel = Mathf.Max(_cloudSave.staminaUpgradeLevel, _localSave.staminaUpgradeLevel);
        compiledSave.chargeUpgradeLevel = Mathf.Max(_cloudSave.chargeUpgradeLevel, _localSave.chargeUpgradeLevel);
        compiledSave.shieldUpgradeLevel = Mathf.Max(_cloudSave.shieldUpgradeLevel, _localSave.shieldUpgradeLevel);
        compiledSave.megaCoinUpgradeLevel = Mathf.Max(_cloudSave.megaCoinUpgradeLevel, _localSave.megaCoinUpgradeLevel);
        compiledSave.powerUpDropUpgradeLevel = Mathf.Max(_cloudSave.powerUpDropUpgradeLevel, _localSave.powerUpDropUpgradeLevel);

        return compiledSave;

    }

    private void SaveData()
    {
        string newSaveData = playerSave.GetSavableData();

        //save string to cloud
        CloudSaving.SaveCloudString(newSaveData, saveString);
        CloudSaving.SaveCloudData(); //need to finish how this actually works

        //then save local
        LocalSaving.SaveLocalString(newSaveData, saveString);
    }

    public void UpdateScore(int _score)
    {
        if (_score > playerSave.highscore)
        {
            playerSave.highscore = _score;
            SaveData();
        }
    }

    public void UpdateCoins(int _change)
    {
        playerSave.coins += _change;
        SaveData();
    }

    public int GetCurrentCoins()
    {
        return playerSave.coins;
    }

    public int GetCurrentHighscore()
    {
        return playerSave.highscore;
    }

    public void UpgradePurchased(Upgrades _upgrade)
    {
        switch (_upgrade)
        {
            case Upgrades.CoinsUpgrade:
                playerSave.coinsUpgradeLevel++;
                if (playerSave.coinsUpgradeLevel > 2)
                {
                    playerSave.coinsUpgradeLevel = 2;
                }
                break;

            case Upgrades.StaminaUpgrade:
                playerSave.staminaUpgradeLevel++;
                if (playerSave.staminaUpgradeLevel > 4)
                {
                    playerSave.staminaUpgradeLevel = 4;
                }
                break;

            case Upgrades.ChargeUpgrade:
                playerSave.chargeUpgradeLevel++;
                if (playerSave.chargeUpgradeLevel > 4)
                {
                    playerSave.chargeUpgradeLevel = 4;
                }
                break;

            case Upgrades.ShieldUpgrade:
                playerSave.shieldUpgradeLevel++;
                if (playerSave.shieldUpgradeLevel > 4)
                {
                    playerSave.shieldUpgradeLevel = 4;
                }
                break;

            case Upgrades.MegaCoinUpgrade:
                playerSave.megaCoinUpgradeLevel++;
                if (playerSave.megaCoinUpgradeLevel > 4)
                {
                    playerSave.megaCoinUpgradeLevel = 4;
                }
                break;

            case Upgrades.PowerUpDropUpgrade:
                playerSave.powerUpDropUpgradeLevel++;
                if (playerSave.powerUpDropUpgradeLevel > 4)
                {
                    playerSave.powerUpDropUpgradeLevel = 4;
                }
                break;

            default:
                Debug.LogError("something wrong in upgradePurchased");
                break;
        }
        SaveData();
    }

    public int GetUpgradeLevel(Upgrades _upgrade)
    {
        int upgradeLevel = 0;
        switch (_upgrade)
        {
            case Upgrades.CoinsUpgrade:
                upgradeLevel = playerSave.coinsUpgradeLevel;
                break;

            case Upgrades.StaminaUpgrade:
                upgradeLevel = playerSave.staminaUpgradeLevel;
                break;

            case Upgrades.ChargeUpgrade:
                upgradeLevel = playerSave.chargeUpgradeLevel;
                break;

            case Upgrades.ShieldUpgrade:
                upgradeLevel = playerSave.shieldUpgradeLevel;
                break;

            case Upgrades.MegaCoinUpgrade:
                upgradeLevel = playerSave.megaCoinUpgradeLevel;
                break;

            case Upgrades.PowerUpDropUpgrade:
                upgradeLevel = playerSave.powerUpDropUpgradeLevel;
                break;

            default:
                Debug.LogError("something wrong in GetUpgradeLevel");
                break;
        }

        return upgradeLevel;
    }

    public void SetSwipeSensitivity(float _sensitivity)
    {
        swipeSensitivity = _sensitivity;
        PlayerPrefs.SetFloat(swipeSensitivityString, _sensitivity);
    }

    public void SetDoubleSwipeSensitivity(float _sensitivity)
    {
        doubleSwipeSensitivity = _sensitivity;
        PlayerPrefs.SetFloat(doubleSwipeSensitivityString, _sensitivity);
    }

    public void SetDoubleSwipe(bool _isOn)
    {
        doubleSwipeOn = _isOn;
        PlayerPrefs.SetInt(doubleSwipeString, _isOn ? 1 : 0);
    }

    public float GetSwipeSensitivity()
    {
        return swipeSensitivity;
    }

    public float GetDoubleSwipeSensitivity()
    {
        return doubleSwipeSensitivity;
    }

    public bool GetDoubleSwipeStatus()
    {
        return doubleSwipeOn;
    }

    public bool CheckIfHighscoore(int _score)
    {
        if (_score > playerSave.highscore)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //debug options
    public void ResetMoney()
    {
        playerSave.coins = 0;
        SaveData();
    }

    public void ResetScore()
    {
        playerSave.highscore = 0;
        SaveData();
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        playerSave = new PlayerSave();
        SaveData();
        SceneLoadingManager.Instance.LoadScene("Initialization");
    }


}
