using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{

    private int currentChargeLevel;
    private int currentBoostLevel;
    private int currentMegaCoinLevel;
    private int currentStaminaLevel;

    public void Initialize()
    {
        SetCurrentPowerUpLevels();
    }

    private void SetCurrentPowerUpLevels()
    {
        currentChargeLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.ChargeUpgrade);
        currentBoostLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.BoostUpgrade);
        currentMegaCoinLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.MegaCoinUpgrade);
        currentStaminaLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.StaminaUpgrade);

    }

    public void ActivatePowerUp(CollectableType _collectableType)
    {
        switch (_collectableType)
        {
            case CollectableType.Gold:
                //points += 10;
               // gameUI.DisplayPoints(points);
               // particleManager.CreateGoldParticles(character.MyTransform.position);
                break;

            case CollectableType.Charge:
                //Debug.Log("got Charge");
                break;

            case CollectableType.Boost:
               // Debug.Log("got boost");
                break;

            case CollectableType.MegaCoin:
               // Debug.Log("got mega coin");
                break;

            case CollectableType.Stamina:
                //need particles
               // character.RestoreChargePower(10f);
                break;

            default:
                Debug.LogError("Unknown Collectable in ActivatePowerUp");
                break;
        }

    }
}
