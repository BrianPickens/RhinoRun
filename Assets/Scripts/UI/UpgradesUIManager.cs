using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UpgradesUIManager : MonoBehaviour
{

    [SerializeField]
    private LoadingTransition loadingScreen = null;

    [SerializeField]
    private PopUpBase screenBlocker = null;

    [SerializeField]
    private GameObject upgradesStore = null;

    [SerializeField]
    private GameObject storeStore = null;

    [SerializeField]
    private UpgradePurchasePopUp purchaseConfirmPopUp = null;

    [SerializeField]
    private PopUpBase purchaseFailPopUp = null;

    [SerializeField]
    private PopUpBase storePurchaseSuccessPopUp = null;

    [SerializeField]
    private PopUpBase storePurchaseFailPopUp = null;

    [SerializeField]
    private Button removeAdsButton = null;

    [SerializeField]
    private Upgrade chargeUpgradeUI = null;

    [SerializeField]
    private Upgrade shieldUpgradeUI = null;

    [SerializeField]
    private Upgrade megaCoinUpgradeUI = null;

    [SerializeField]
    private Upgrade staminaUpgradeUI = null;

    [SerializeField]
    private Upgrade powerUpDropUpgradeUI = null;

    [SerializeField]
    private Upgrade coinUpgradeUI = null;

    [SerializeField]
    private Text coinText = null;

    [SerializeField]
    private AudioClip upgradeSound = null;

    public Action OnMenuPress;
    public Action<Upgrades, int> OnUpgradePurchase;
    public Action OnPurchaseConfirm;

    public void Init()
    {
        loadingScreen.StartWithLoading();
        ShowUpgradesStore();
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
        if (_amount == 0)
        {
            coinText.text = "" + _amount;
        }
        else
        {
            string coinString = _amount.ToString("#,#");

            coinText.text = coinString;
        }
    }

    public void ShowPurchaseConfirmation(Upgrades _upgradeType, int _price)
    {
        screenBlocker.OpenPopUp();
        purchaseConfirmPopUp.OpenPurchasePopUp(_upgradeType, _price);
    }

    public void PurchasedConfirmed()
    {
        PlaySound(upgradeSound);
        purchaseConfirmPopUp.ExitPopUp();
        screenBlocker.ExitPopUp();
        if (OnPurchaseConfirm != null)
        {
            OnPurchaseConfirm();
        }
    }

    public void PurchaseDenied()
    {
        PlayClickSound();
        purchaseConfirmPopUp.ExitPopUp();
        screenBlocker.ExitPopUp();
    }

    public void ShowPurchaseFailure()
    {
        screenBlocker.ExitPopUp();
        purchaseFailPopUp.OpenPopUp();
    }

    public void ClosePurchaseFailure()
    {
        PlayClickSound();
        purchaseFailPopUp.ExitPopUp();
        screenBlocker.ExitPopUp();
    }

    private void BuyUpgrade(Upgrades _upgradeType, int _price)
    {
        PlayClickSound();
        if (OnUpgradePurchase != null)
        {
            OnUpgradePurchase(_upgradeType, _price);
        }
    }

    public void ShowUpgradesStore()
    {
        PlayClickSound();
        storeStore.SetActive(false);
        upgradesStore.SetActive(true);
    }

    public void ShowStoreStore()
    {
        PlayClickSound();
        upgradesStore.SetActive(false);
        storeStore.SetActive(true);
    }

    public void ShowCompletedStorePurchase()
    {
        screenBlocker.OpenPopUp();
        storePurchaseSuccessPopUp.OpenPopUp();
    }

    public void HideCompletedStorePurchase()
    {
        PlayClickSound();
        screenBlocker.ExitPopUp();
        storePurchaseSuccessPopUp.ExitPopUp();
    }

    public void ShowStorePurchaseFailed()
    {
        screenBlocker.OpenPopUp();
        storePurchaseFailPopUp.OpenPopUp();
    }

    public void HideStorePurchaseFailed()
    {
        PlayClickSound();
        screenBlocker.ExitPopUp();
        storePurchaseFailPopUp.ExitPopUp();
    }

    public void DisableRemoveAdsButton()
    {
        removeAdsButton.interactable = false;
    }

    public void EnableRemoveAdsButton()
    {
        removeAdsButton.interactable = true;
    }

    private IEnumerator SceneLoadDelay(Action _callback)
    {
        yield return new WaitForSeconds(1f);
        _callback();
    }

    private void PlaySound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
        }
    }

    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayClickSound();
        }
    }
}
