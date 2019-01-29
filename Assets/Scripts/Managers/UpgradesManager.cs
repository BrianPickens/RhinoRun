using System.Collections;
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

    private int currentUpgradePrice;
    private Upgrades currentUpgradeType;

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

        upgradesUI.OnUpgradePurchase = null;
        upgradesUI.OnUpgradePurchase += UpgradePurchased;

        upgradesUI.OnPurchaseConfirm = null;
        upgradesUI.OnPurchaseConfirm += CompletePurchase;

        int currentCoins = SaveManager.Instance.GetCurrentCoins();
        upgradesUI.UpdateCurrency(currentCoins);

        GetCurrentUpgradeLevels();
        SetCurrentUpgrades();
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

    private void UpgradePurchased(Upgrades _upgradeType, int _price)
    {
        int currentCoins = SaveManager.Instance.GetCurrentCoins();
        currentUpgradePrice = _price;
        currentUpgradeType = _upgradeType;

        if (_price < currentCoins)
        {
            upgradesUI.ShowPurchaseConfirmation();
        }
        else
        {
            upgradesUI.ShowPurchaseFailure();
        }
    }

    private void CompletePurchase()
    {
        SaveManager.Instance.UpgradePurchased(currentUpgradeType);
        SaveManager.Instance.UpdateCoins(-currentUpgradePrice);
        GetCurrentUpgradeLevels();
        SetCurrentUpgrades();
        int currentCoins = SaveManager.Instance.GetCurrentCoins();
        upgradesUI.UpdateCurrency(currentCoins);

    }
    
    //debug buttons
    public void IncreaseCoinUpgrade()
    {
        SaveManager.Instance.UpgradePurchased(Upgrades.CoinsUpgrade);
    }

    public void IncreaseStaminaUpgrade()
    {
        SaveManager.Instance.UpgradePurchased(Upgrades.StaminaUpgrade);
    }

    public void IncreaseChargeUpgrade()
    {
        SaveManager.Instance.UpgradePurchased(Upgrades.ChargeUpgrade);
    }

    public void IncreaseShieldUpgrade()
    {
        SaveManager.Instance.UpgradePurchased(Upgrades.ShieldUpgrade);
    }

    public void IncreaseMegaCoinUpgrade()
    {
        SaveManager.Instance.UpgradePurchased(Upgrades.MegaCoinUpgrade);
    }

    public void IncreaseDropsUpgrade()
    {
        SaveManager.Instance.UpgradePurchased(Upgrades.PowerUpDropUpgrade);
    }
    //end debug buttons
}
