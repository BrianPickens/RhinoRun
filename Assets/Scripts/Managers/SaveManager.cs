using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrades { StaminaUpgrade, CoinsUpgrade }
public class SaveManager : MonoBehaviour
{

    private static SaveManager instance = null;
    public static SaveManager Instance
    {
        get { return instance; }
    }

    private int coins;

    private int highscore;

    private int coinsUpgradeLevel;

    private int staminaUpgradeLevel;

    private const string highscoreString = "highscore";

    private const string coinsString = "coins";

    private const string coinUpgradeString = "coinUpgrade";

    private const string staminaUpgradeString = "staminaUpgrade";

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

            default:
                Debug.LogError("something wrong in GetUpgradeLevel");
                break;
        }

        return upgradeLevel;
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



}
