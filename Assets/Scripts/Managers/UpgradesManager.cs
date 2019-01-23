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


//pick up types
//coins
//unlimited cgarge
//rhino snacks
//Shield
//mega coin

public class UpgradesManager : MonoBehaviour
{

    [SerializeField]
    private UpgradesUIManager upgradesUI;

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

        upgradesUI.Init();

        upgradesUI.OnMenuPress = null;
        upgradesUI.OnMenuPress += ReturnToMain;
    }

    public void ReturnToMain()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("MainMenu");
        }
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
