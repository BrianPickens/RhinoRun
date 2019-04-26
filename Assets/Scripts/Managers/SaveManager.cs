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

    [SerializeField]
    private CloudSaveAssistant cloudSaveAssistant;

    private PlayerSave playerSave;

    public const string saveString = "gameSaveData";

    private bool dataLoaded = false;

    private bool tutorialCompleted = false;

    private const string tutorialCompleteString = "tutorialCompleted";

    private bool hasRemoveAds;

    private const string hasRemoveAdsString = "hasRemoveAds";

    private int levelOneCost = 1000;
    private int levelTwoCost = 10000;
    private int levelThreeCost = 25000;
    private int levelFourCost = 50000;

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
        StartCoroutine(InitializeRoutine());


    }

    private IEnumerator InitializeRoutine()
    {
        StartCoroutine(GetPlayerData());

        while (!dataLoaded)
        {
            yield return null;
        }

        initialized = true;
    }

    private IEnumerator GetPlayerData()
    {

        playerSave = new PlayerSave();

#if UNITY_EDITOR

        PlayerSave localSave = LoadLocalData();
        PlayerSave cloudSave = new PlayerSave();
        yield return null;

#elif UNITY_IOS
       // Debug.Log("CLOUD LOAD");
        PlayerSave localSave = LoadLocalData();
        PlayerSave cloudSave = new PlayerSave();
        cloudSaveAssistant.LoadCloudSaveData(saveString);
        while (cloudSaveAssistant.Loading)
        {
            yield return null;
        }

        string loadedCloudData = cloudSaveAssistant.LoadedData;

        if (loadedCloudData != "loading" && loadedCloudData != null)
        {
            cloudSave.ConvertDataFromString(loadedCloudData);
           // Debug.Log("cloud Data Loaded");
        }
#endif
        PlayerSave bestSave = CompareCloudAndLocalData(cloudSave, localSave);

        playerSave = bestSave;
        dataLoaded = true;
    }

    private PlayerSave LoadLocalData()
    {
        PlayerSave localSave = new PlayerSave();

        string localSaveData = LocalSaving.GetLocalString(saveString);

        localSave.ConvertDataFromString(localSaveData);

        //only saved local
        tutorialCompleted = LocalSaving.GetLocalBool(tutorialCompleteString, 0);
        hasRemoveAds = LocalSaving.GetLocalBool(hasRemoveAdsString, 0);
        //end only saved local

        // Debug.Log("local data loaded");

        return localSave;
    }

    //compare local and cloud save and give player the save with the most value
    private PlayerSave CompareCloudAndLocalData(PlayerSave _cloudSave, PlayerSave _localSave)
    {

        int localSaveValue = CompilePlayerSaveValue(_localSave);
        int cloudSaveValue = CompilePlayerSaveValue(_cloudSave);

        if (localSaveValue >= cloudSaveValue)
        {
            return _localSave;
        }
        else
        {
            return _cloudSave;
        }

    }

    //determines the coin value of a player save
    private int CompilePlayerSaveValue(PlayerSave _playersave)
    {
        int value = 0;

        value += _playersave.coins;
        value += GetUpgradeValue(_playersave.coinsUpgradeLevel);
        value += GetUpgradeValue(_playersave.staminaUpgradeLevel);
        value += GetUpgradeValue(_playersave.chargeUpgradeLevel);
        value += GetUpgradeValue(_playersave.shieldUpgradeLevel);
        value += GetUpgradeValue(_playersave.megaCoinUpgradeLevel);
        value += GetUpgradeValue(_playersave.powerUpDropUpgradeLevel);

        return value;
    }

    private int GetUpgradeValue(int _level)
    {
        int upgradeValue = 0;
        switch (_level)
        {
            case 0:
                upgradeValue = 0;
                break;

            case 1:
                upgradeValue = levelOneCost;
                break;

            case 2:
                upgradeValue = levelOneCost + levelTwoCost;
                break;

            case 3:
                upgradeValue = levelOneCost + levelTwoCost + levelThreeCost;
                break;

            case 4:
                upgradeValue = levelOneCost + levelTwoCost + levelThreeCost + levelFourCost;
                break;

            default:
                upgradeValue = 0;
                Debug.Log("invalid level passed to GetUpgradeValue");
                break;
        }

        return upgradeValue;

    }

    private void SaveData()
    {
        string newSaveData = playerSave.GetSavableData();

#if UNITY_EDITOR
        //save local
        LocalSaving.SaveLocalString(newSaveData, saveString);
#elif UNITY_IOS
        //save local
        LocalSaving.SaveLocalString(newSaveData, saveString);
        //save string to cloud
        cloudSaveAssistant.CreateNewCloudSaveAttempt(newSaveData, saveString);
#endif

    }

    public void UpdateScore(int _score)
    {
        if (_score > playerSave.highscore)
        {
#if UNITY_IOS
            GameCenter.PostScoreToLeaderBoard(_score);
#endif
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

    public void SetHasRemoveAds(bool _hasRemoveAds)
    {
        hasRemoveAds = _hasRemoveAds;
        if (_hasRemoveAds)
        {
            LocalSaving.SaveLocalBool(true, hasRemoveAdsString);
        }
        else
        {
            LocalSaving.SaveLocalBool(false, hasRemoveAdsString);
        }
    }

    public bool GetHasRemoveAdsStatus()
    {
        return hasRemoveAds;
    }

    public void SetTutorialCompleted()
    {
        tutorialCompleted = true;
        LocalSaving.SaveLocalBool(true, tutorialCompleteString);
    }

    public bool GetTutorialStatus()
    {
        return tutorialCompleted;
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
        SceneLoadingManager.Instance.LoadScene("MainMenu");
    }


}
