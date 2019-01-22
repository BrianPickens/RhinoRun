using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrades { StaminaUpgrade, CoinsUpgrade, ChargeUpgrade, ShieldUpgrade, MegaCoinUpgrade, PowerUpDropUpgrade }
public class SaveManager : MonoBehaviour
{

    private static SaveManager instance = null;
    public static SaveManager Instance
    {
        get { return instance; }
    }

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
        GetStats();
        initialized = true;

    }

    private void GetStats()
    {
        if (PlayerPrefs.HasKey(coinsString))
        {
            coins = PlayerPrefs.GetInt(coinsString);
        }
        else
        {
            coins = 0;
        }

        if (PlayerPrefs.HasKey(highscoreString))
        {
            highscore = PlayerPrefs.GetInt(highscoreString);
        }
        else
        {
            highscore = 0;
        }

        if (PlayerPrefs.HasKey(swipeSensitivityString))
        {
            swipeSensitivity = PlayerPrefs.GetFloat(swipeSensitivityString);
        }
        else
        {
            swipeSensitivity = 0.2f;
        }

        if (PlayerPrefs.HasKey(doubleSwipeSensitivityString))
        {
            doubleSwipeSensitivity = PlayerPrefs.GetFloat(doubleSwipeSensitivityString);
        }
        else
        {
            doubleSwipeSensitivity = 3f;
        }

        if (PlayerPrefs.HasKey(doubleSwipeString))
        {
            doubleSwipeOn = PlayerPrefs.GetInt(doubleSwipeString) == 1 ? true : false;
        }
        else
        {
            doubleSwipeOn = true;
        }

        if (PlayerPrefs.HasKey(coinUpgradeString))
        {
            coinsUpgradeLevel = PlayerPrefs.GetInt(coinUpgradeString);
        }
        else
        {
            coinsUpgradeLevel = 0;
        }

        if (PlayerPrefs.HasKey(staminaUpgradeString))
        {
            staminaUpgradeLevel = PlayerPrefs.GetInt(staminaUpgradeString);
        }
        else
        {
            staminaUpgradeLevel = 0;
        }

        if (PlayerPrefs.HasKey(chargeUpgradeString))
        {
            chargeUpgradeLevel = PlayerPrefs.GetInt(chargeUpgradeString);
        }
        else
        {
            chargeUpgradeLevel = 0;
        }

        if (PlayerPrefs.HasKey(shieldUpgradeString))
        {
            shieldUpgradeLevel = PlayerPrefs.GetInt(shieldUpgradeString);
        }
        else
        {
            shieldUpgradeLevel = 1;
        }

        if (PlayerPrefs.HasKey(megaCoinUpgradeString))
        {
            megaCoinUpgradeLevel = PlayerPrefs.GetInt(megaCoinUpgradeString);
        }
        else
        {
            megaCoinUpgradeLevel = 0;
        }

        if (PlayerPrefs.HasKey(powerUpDropUpgradeString))
        {
            powerUpDropUpgradeLevel = PlayerPrefs.GetInt(powerUpDropUpgradeString);
        }
        else
        {
            powerUpDropUpgradeLevel = 0;
        }

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

    public void UpgradePurchased(Upgrades _upgrade, int _level)
    {
        switch (_upgrade)
        {
            case Upgrades.CoinsUpgrade:
                coinsUpgradeLevel = _level;
                PlayerPrefs.SetInt(coinUpgradeString, _level);
                break;

            case Upgrades.StaminaUpgrade:
                staminaUpgradeLevel = _level;
                PlayerPrefs.SetInt(staminaUpgradeString, _level);
                break;

            case Upgrades.ChargeUpgrade:
                chargeUpgradeLevel = _level;
                PlayerPrefs.SetInt(chargeUpgradeString, _level);
                break;

            case Upgrades.ShieldUpgrade:
                shieldUpgradeLevel = _level;
                PlayerPrefs.SetInt(shieldUpgradeString, _level);
                break;

            case Upgrades.MegaCoinUpgrade:
                megaCoinUpgradeLevel = _level;
                PlayerPrefs.SetInt(megaCoinUpgradeString, _level);
                break;

            case Upgrades.PowerUpDropUpgrade:
                powerUpDropUpgradeLevel = _level;
                PlayerPrefs.SetInt(powerUpDropUpgradeString, _level);
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
