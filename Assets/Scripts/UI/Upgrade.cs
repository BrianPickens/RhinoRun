using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private Upgrades upgradeType;

    [SerializeField]
    private string upgradeName;

    [SerializeField]
    private int maxLevels;

    [SerializeField]
    private Text upgradeNameText;

    [SerializeField]
    private Text currentLevelInfo;

    [SerializeField]
    private Text nextLevelInfo;

    [SerializeField]
    private Text priceText;

    [SerializeField]
    private Sprite unlockedBackground;

    [SerializeField]
    private List<int> upgradePrices = new List<int>();

    [SerializeField]
    private List<Image> upgradeLevels = new List<Image>();

    [SerializeField]
    [TextArea]
    private List<string> levelInfo = new List<string>();

    private int currentPrice;

    private int currentLevel;

    public Action<Upgrades, int> OnUpgradePurchase;

    public void SetUpgradeInfo(int _currentLevel)
    {
        currentLevel = _currentLevel;
        upgradeNameText.text = upgradeName;
        SetUpgradeText(_currentLevel);
        SetUpgradeLevelImages(_currentLevel);
        SetPrice(_currentLevel);
    }

    private void SetUpgradeText(int _currentLevel)
    {
        currentLevelInfo.text = "" + levelInfo[_currentLevel];
        if (_currentLevel < maxLevels)
        {
            nextLevelInfo.text = "" + levelInfo[_currentLevel + 1];
        }
        else
        {
            nextLevelInfo.text = "--";
        }
    }

    private void SetUpgradeLevelImages(int _currentLevel)
    {
        for (int i = 0; i < _currentLevel; i++)
        {
            upgradeLevels[i].sprite = unlockedBackground;
        }

        for (int i = 0; i < upgradeLevels.Count; i++)
        {
            if (i >= maxLevels)
            {
                upgradeLevels[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetPrice(int _currentLevel)
    {
        if (_currentLevel < maxLevels)
        {
            currentPrice = upgradePrices[_currentLevel];
            priceText.text = "" + currentPrice;
        }
        else
        {
            currentPrice = 0;
            priceText.text = " --";
        }
    }

    public void BuyUpgrade()
    {
        if (currentLevel < maxLevels)
        {
            if (OnUpgradePurchase != null)
            {
                OnUpgradePurchase(upgradeType, currentPrice);
            }
        }
    }
}
