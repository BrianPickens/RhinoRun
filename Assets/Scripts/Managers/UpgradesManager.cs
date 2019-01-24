﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//upgrade types
//increase coin amount
//unlimited charge
//rhino snax boost
//shield
//mega coin
//powerup drpo frequency


public class UpgradesManager : MonoBehaviour
{

    [SerializeField]
    private UpgradesUIManager upgradesUI;

    private int currentChargeLevel;
    private int currentShieldLevel;
    private int currentMegaCoinLevel;
    private int currentStaminaLevel;
    private int currentPowerUpDropLevel;
    private int currentCoinLevel;

    private void Start()
    {
        Init();   
    }

    private void Init()
    {
        if (InitializationManager.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Initialization");
        }

        GetCurrentUpgradeLevels();

        upgradesUI.Init();

        upgradesUI.OnMenuPress = null;
        upgradesUI.OnMenuPress += ReturnToMain;
    }

    private void GetCurrentUpgradeLevels()
    {
        currentChargeLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.ChargeUpgrade);
        currentShieldLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.ShieldUpgrade);
        currentMegaCoinLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.MegaCoinUpgrade);
        currentStaminaLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.StaminaUpgrade);
        currentPowerUpDropLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.PowerUpDropUpgrade);
        currentCoinLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.CoinsUpgrade);
    }

    private void SetCurrentUpgrades()
    {
        upgradesUI.InitializeUpgrade(Upgrades.ChargeUpgrade, currentChargeLevel);
        upgradesUI.InitializeUpgrade(Upgrades.ShieldUpgrade, currentShieldLevel);
        upgradesUI.InitializeUpgrade(Upgrades.MegaCoinUpgrade, currentMegaCoinLevel);
        upgradesUI.InitializeUpgrade(Upgrades.StaminaUpgrade, currentStaminaLevel);
        upgradesUI.InitializeUpgrade(Upgrades.PowerUpDropUpgrade, currentPowerUpDropLevel);
        upgradesUI.InitializeUpgrade(Upgrades.CoinsUpgrade, currentCoinLevel);
    }

    public void ReturnToMain()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("MainMenu");
        }
    }

    private void UpgradePurchased(Upgrades _upgradeType)
    {

    }
    
    //debug buttons
    public void IncreaseCoinUpgrade()
    {
        int level = SaveManager.Instance.GetUpgradeLevel(Upgrades.CoinsUpgrade);
        level++;
        if (level > 2)
        {
            level = 2;
        }
        SaveManager.Instance.UpgradePurchased(Upgrades.CoinsUpgrade, level);
    }

    public void IncreaseStaminaUpgrade()
    {
        int level = SaveManager.Instance.GetUpgradeLevel(Upgrades.StaminaUpgrade);
        level++;
        if (level > 4)
        {
            level = 4;
        }
        SaveManager.Instance.UpgradePurchased(Upgrades.StaminaUpgrade, level);
    }

    public void IncreaseChargeUpgrade()
    {
        int level = SaveManager.Instance.GetUpgradeLevel(Upgrades.ChargeUpgrade);
        level++;
        if (level > 4)
        {
            level = 4;
        }
        SaveManager.Instance.UpgradePurchased(Upgrades.ChargeUpgrade, level);
    }

    public void IncreaseShieldUpgrade()
    {
        int level = SaveManager.Instance.GetUpgradeLevel(Upgrades.ShieldUpgrade);
        level++;
        if (level > 4)
        {
            level = 4;
        }
        SaveManager.Instance.UpgradePurchased(Upgrades.ShieldUpgrade, level);
    }

    public void IncreaseMegaCoinUpgrade()
    {
        int level = SaveManager.Instance.GetUpgradeLevel(Upgrades.MegaCoinUpgrade);
        level++;
        if (level > 4)
        {
            level = 4;
        }
        SaveManager.Instance.UpgradePurchased(Upgrades.MegaCoinUpgrade, level);
    }

    public void IncreaseDropsUpgrade()
    {
        int level = SaveManager.Instance.GetUpgradeLevel(Upgrades.PowerUpDropUpgrade);
        level++;
        if (level > 4)
        {
            level = 4;
        }
        SaveManager.Instance.UpgradePurchased(Upgrades.PowerUpDropUpgrade, level);
    }
    //end debug buttons
}
