using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradesUIManager : MonoBehaviour
{

    [SerializeField]
    private LoadingTransition loadingScreen;

    [SerializeField]
    private Upgrade chargeUpgradeUI;

    [SerializeField]
    private Upgrade shieldUpgradeUI;

    [SerializeField]
    private Upgrade megaCoinUpgradeUI;

    [SerializeField]
    private Upgrade staminaUpgradeUI;

    [SerializeField]
    private Upgrade powerUpDropUpgradeUI;

    [SerializeField]
    private Upgrade coinUpgradeUI;

    public Action OnMenuPress;

    public void Init()
    {
        loadingScreen.StartWithLoading();
        loadingScreen.HideLoading();
    }

    public void MenuButtonPress()
    {
        loadingScreen.ShowLoading();
        if (OnMenuPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnMenuPress));
        }
    }

    public void InitializeUpgrade(Upgrades _upgrade, int _currentLevel)
    {
        switch (_upgrade)
        {
            case Upgrades.ChargeUpgrade:
                chargeUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.ShieldUpgrade:
                shieldUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.MegaCoinUpgrade:
                megaCoinUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.StaminaUpgrade:
                staminaUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.PowerUpDropUpgrade:
                powerUpDropUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.CoinsUpgrade:
                coinUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            default:
                Debug.LogError("invalide upgrade in initializeUpgrade");
                break;
        }
    }

    private IEnumerator SceneLoadDelay(Action _callback)
    {
        yield return new WaitForSeconds(1f);
        _callback();
    }
}
