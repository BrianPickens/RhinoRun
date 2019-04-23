using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePurchasePopUp : PopUpBase
{

    [SerializeField]
    private Text upgradeText = null;

    [SerializeField]
    private Text upgradePriceText = null;

    public void OpenPurchasePopUp(Upgrades _upgradeType, int _price)
    {
        switch (_upgradeType)
        {
            case Upgrades.ChargeUpgrade:
                upgradeText.text = "Charge Upgrade";
                break;

            case Upgrades.ShieldUpgrade:
                upgradeText.text = "Shield Upgrade";
                break;

            case Upgrades.MegaCoinUpgrade:
                upgradeText.text = "Diamond Upgrade";
                break;

            case Upgrades.StaminaUpgrade:
                upgradeText.text = "Stamina Upgrade";
                break;

            case Upgrades.PowerUpDropUpgrade:
                upgradeText.text = "Rate Upgrade";
                break;

            case Upgrades.CoinsUpgrade:
                upgradeText.text = "Coin Upgrade";
                break;

            default:
                Debug.LogError("invalide upgrade in upgrades Pop Up");
                break;
        }

        string priceString = _price.ToString("#,#");

        upgradePriceText.text = priceString;

        OpenPopUp();
    }

}
