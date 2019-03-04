using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerSave
{
    public int coins;
    public int highscore;
    public int coinsUpgradeLevel;
    public int staminaUpgradeLevel;
    public int chargeUpgradeLevel;
    public int shieldUpgradeLevel;
    public int megaCoinUpgradeLevel;
    public int powerUpDropUpgradeLevel;

}

public enum Upgrades { StaminaUpgrade, CoinsUpgrade, ChargeUpgrade, ShieldUpgrade, MegaCoinUpgrade, PowerUpDropUpgrade }
public class SaveManager : MonoBehaviour
{



    private static SaveManager instance = null;
    public static SaveManager Instance
    {
        get { return instance; }
    }

    private PlayerSave playerSave;

    private int coins;

    private int highscore;

    private float swipeSensitivity;

    private float doubleSwipeSensitivity;

    private bool doubleSwipeOn;

    private int coinsUpgradeLevel;

    private int staminaUpgradeLevel;

    private int chargeUpgradeLevel;

    private int shieldUpgradeLevel;

    private int megaCoinUpgradeLevel;

    private int powerUpDropUpgradeLevel;

    private const string highscoreString = "highscore";

    private const string coinsString = "coins";

    private const string coinUpgradeString = "coinUpgrade";

    private const string staminaUpgradeString = "staminaUpgrade";

    private const string chargeUpgradeString = "chargeUpgrade";

    private const string shieldUpgradeString = "shieldUpgrade";

    private const string megaCoinUpgradeString = "megaCoinUpgrade";

    private const string powerUpDropUpgradeString = "powerUpDropUpgrade";

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

        string[] val = { "1", "2", "3", "4", "5" };
        string sep = ",";
        string result;

        result = string.Join(sep, val);
        Debug.Log(result);
        //PlayerSave cloudSave = LoadCloudData();
        //PlayerSave localSave = LoadLocalData();
        //PlayerSave compiledSave = CompareCloudAndLocalData(cloudSave, localSave);

        //playerSave = compiledSave;

    }

    private PlayerSave LoadCloudData()
    {
        CloudSaving.GatherCloudData();
        PlayerSave cloudSave = new PlayerSave();
        cloudSave.coins = CloudSaving.GetCloudInt(coinsString);
        cloudSave.highscore = CloudSaving.GetCloudInt(highscoreString);
        cloudSave.coinsUpgradeLevel = CloudSaving.GetCloudInt(coinUpgradeString);
        cloudSave.staminaUpgradeLevel = CloudSaving.GetCloudInt(staminaUpgradeString);
        cloudSave.chargeUpgradeLevel = CloudSaving.GetCloudInt(chargeUpgradeString);
        cloudSave.shieldUpgradeLevel = CloudSaving.GetCloudInt(shieldUpgradeString);
        cloudSave.megaCoinUpgradeLevel = CloudSaving.GetCloudInt(megaCoinUpgradeString);
        cloudSave.powerUpDropUpgradeLevel = CloudSaving.GetCloudInt(powerUpDropUpgradeString);

        return cloudSave;
    }

    private PlayerSave LoadLocalData()
    {
        PlayerSave localSave = new PlayerSave();
        localSave.coins = LocalSaving.GetLocalInt(coinsString, 0);
        localSave.highscore = LocalSaving.GetLocalInt(highscoreString, 0);
        localSave.coinsUpgradeLevel = LocalSaving.GetLocalInt(coinUpgradeString, 0);
        localSave.staminaUpgradeLevel = LocalSaving.GetLocalInt(staminaUpgradeString, 0);
        localSave.chargeUpgradeLevel = LocalSaving.GetLocalInt(chargeUpgradeString, 0);
        localSave.shieldUpgradeLevel = LocalSaving.GetLocalInt(shieldUpgradeString, 1); //shield upgrade is always 1
        localSave.megaCoinUpgradeLevel = LocalSaving.GetLocalInt(megaCoinUpgradeString, 0);
        localSave.powerUpDropUpgradeLevel = LocalSaving.GetLocalInt(powerUpDropUpgradeString, 0);

        //only saved local
        swipeSensitivity = LocalSaving.GetLocalFloat(swipeSensitivityString, 0.2f);
        doubleSwipeSensitivity = LocalSaving.GetLocalFloat(doubleSwipeSensitivityString, 3f);
        doubleSwipeOn = LocalSaving.GetLocalBool(doubleSwipeString, 1);
        //end only saved local

        return localSave;
    }

    private PlayerSave CompareCloudAndLocalData(PlayerSave _cloudSave, PlayerSave _localSave)
    {
        PlayerSave compiledSave = new PlayerSave();
        compiledSave.coins = Mathf.Max(_cloudSave.coins, _localSave.coins);
        compiledSave.highscore = Mathf.Max(_cloudSave.highscore, _localSave.highscore);
        compiledSave.coinsUpgradeLevel = Mathf.Max(_cloudSave.coinsUpgradeLevel, _localSave.coinsUpgradeLevel);


        return compiledSave;

    }

    private void SaveData()
    {
        //save cloud
        //then save local
    }

    private void SaveCloud()
    {

    }

    private void SaveLocal()
    {

    }

    public void UpdateScore(int _score)
    {
        if (_score > highscore)
        {
            PlayerPrefs.SetInt(highscoreString, _score);
            highscore = _score;
        }
    }

    public void UpdateCoins(int _change)
    {
        coins += _change;
        PlayerPrefs.SetInt(coinsString, coins);
    }

    public int GetCurrentCoins()
    {
        return coins;
    }

    public int GetCurrentHighscore()
    {
        return highscore;
    }

    public void UpgradePurchased(Upgrades _upgrade)
    {
        switch (_upgrade)
        {
            case Upgrades.CoinsUpgrade:
                coinsUpgradeLevel++;
                if (coinsUpgradeLevel > 2)
                {
                    coinsUpgradeLevel = 2;
                }
                PlayerPrefs.SetInt(coinUpgradeString, coinsUpgradeLevel);
                break;

            case Upgrades.StaminaUpgrade:
                staminaUpgradeLevel++;
                if (staminaUpgradeLevel > 4)
                {
                    staminaUpgradeLevel = 4;
                }
                PlayerPrefs.SetInt(staminaUpgradeString, staminaUpgradeLevel);
                break;

            case Upgrades.ChargeUpgrade:
                chargeUpgradeLevel++;
                if (chargeUpgradeLevel > 4)
                {
                    chargeUpgradeLevel = 4;
                }
                PlayerPrefs.SetInt(chargeUpgradeString, chargeUpgradeLevel);
                break;

            case Upgrades.ShieldUpgrade:
                shieldUpgradeLevel++;
                if (shieldUpgradeLevel > 4)
                {
                    shieldUpgradeLevel = 4;
                }
                PlayerPrefs.SetInt(shieldUpgradeString, shieldUpgradeLevel);
                break;

            case Upgrades.MegaCoinUpgrade:
                megaCoinUpgradeLevel++;
                if (megaCoinUpgradeLevel > 4)
                {
                    megaCoinUpgradeLevel = 4;
                }
                PlayerPrefs.SetInt(megaCoinUpgradeString, megaCoinUpgradeLevel);
                break;

            case Upgrades.PowerUpDropUpgrade:
                powerUpDropUpgradeLevel++;
                if (powerUpDropUpgradeLevel > 4)
                {
                    powerUpDropUpgradeLevel = 4;
                }
                PlayerPrefs.SetInt(powerUpDropUpgradeString, powerUpDropUpgradeLevel);
                break;

            default:
                Debug.LogError("something wrong in upgradePurchased");
                break;
        }
    }

    public int GetUpgradeLevel(Upgrades _upgrade)
    {
        int upgradeLevel = 0;
        switch (_upgrade)
        {
            case Upgrades.CoinsUpgrade:
                upgradeLevel = coinsUpgradeLevel;
                break;

            case Upgrades.StaminaUpgrade:
                upgradeLevel = staminaUpgradeLevel;
                break;

            case Upgrades.ChargeUpgrade:
                upgradeLevel = chargeUpgradeLevel;
                break;

            case Upgrades.ShieldUpgrade:
                upgradeLevel = shieldUpgradeLevel;
                break;

            case Upgrades.MegaCoinUpgrade:
                upgradeLevel = megaCoinUpgradeLevel;
                break;

            case Upgrades.PowerUpDropUpgrade:
                upgradeLevel = powerUpDropUpgradeLevel;
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
        if (_score > highscore)
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
        coins = 0;
        PlayerPrefs.SetInt(coinsString, coins);
    }

    public void ResetScore()
    {
        highscore = 0;
        PlayerPrefs.SetInt(highscoreString, highscore);
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneLoadingManager.Instance.LoadScene("Initialization");
    }


}
