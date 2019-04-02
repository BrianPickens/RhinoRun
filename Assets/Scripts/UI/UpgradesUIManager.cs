using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UpgradesUIManager : MonoBehaviour
{

    [SerializeField]
    private LoadingTransition loadingScreen;

    [SerializeField]
    private GameObject screenBlocker;

    [SerializeField]
    private GameObject purchaseConfirmPopUp;

    [SerializeField]
    private GameObject purchaseFailPopUp;

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

    [SerializeField]
    private Text coinText;

    public Action OnMenuPress;
    public Action<Upgrades, int> OnUpgradePurchase;
    public Action OnPurchaseConfirm;

    public void Init()
    {
        loadingScreen.StartWithLoading();
        loadingScreen.HideLoading();
    }

    public void MenuButtonPress()
    {
        PlayClickSound();
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
                chargeUpgradeUI.OnUpgradePurchase = null;
                chargeUpgradeUI.OnUpgradePurchase += BuyUpgrade;
                chargeUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.ShieldUpgrade:
                shieldUpgradeUI.OnUpgradePurchase = null;
                shieldUpgradeUI.OnUpgradePurchase += BuyUpgrade;
                shieldUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.MegaCoinUpgrade:
                megaCoinUpgradeUI.OnUpgradePurchase = null;
                megaCoinUpgradeUI.OnUpgradePurchase += BuyUpgrade;
                megaCoinUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.StaminaUpgrade:
                staminaUpgradeUI.OnUpgradePurchase = null;
                staminaUpgradeUI.OnUpgradePurchase += BuyUpgrade;
                staminaUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.PowerUpDropUpgrade:
                powerUpDropUpgradeUI.OnUpgradePurchase = null;
                powerUpDropUpgradeUI.OnUpgradePurchase += BuyUpgrade;
                powerUpDropUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            case Upgrades.CoinsUpgrade:
                coinUpgradeUI.OnUpgradePurchase = null;
                coinUpgradeUI.OnUpgradePurchase += BuyUpgrade;
                coinUpgradeUI.SetUpgradeInfo(_currentLevel);
                break;

            default:
                Debug.LogError("invalide upgrade in initializeUpgrade");
                break;
        }
    }

    public void UpdateCurrency(int _amount)
    {
        coinText.text = "" + _amount;
    }

    public void ShowPurchaseConfirmation()
    {
        screenBlocker.gameObject.SetActive(true);
        purchaseConfirmPopUp.gameObject.SetActive(true);
    }

    public void PurchasedConfirmed()
    {
        PlayClickSound();
        purchaseConfirmPopUp.gameObject.SetActive(false);
        screenBlocker.gameObject.SetActive(false);
        if (OnPurchaseConfirm != null)
        {
            OnPurchaseConfirm();
        }
    }

    public void PurchaseDenied()
    {
        PlayClickSound();
        purchaseConfirmPopUp.gameObject.SetActive(false);
        screenBlocker.gameObject.SetActive(false);
    }

    public void ShowPurchaseFailure()
    {
        screenBlocker.gameObject.SetActive(false);
        purchaseFailPopUp.gameObject.SetActive(true);
    }

    public void ClosePurchaseFailure()
    {
        PlayClickSound();
        purchaseFailPopUp.gameObject.SetActive(false);
        screenBlocker.gameObject.SetActive(false);
    }

    private void BuyUpgrade(Upgrades _upgradeType, int _price)
    {
        PlayClickSound();
        if (OnUpgradePurchase != null)
        {
            OnUpgradePurchase(_upgradeType, _price);
        }
    }

    private IEnumerator SceneLoadDelay(Action _callback)
    {
        yield return new WaitForSeconds(1f);
        _callback();
    }

    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayClickSound();
        }
    }
}
