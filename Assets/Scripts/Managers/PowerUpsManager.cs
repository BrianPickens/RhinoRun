using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField]
    private Character character;

    private int currentChargeLevel;
    private int currentShieldLevel;
    private int currentMegaCoinLevel;
    private int currentStaminaLevel;

    public void Initialize()
    {
        SetCurrentPowerUpLevels();
    }

    private void SetCurrentPowerUpLevels()
    {
        currentChargeLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.ChargeUpgrade);
        currentShieldLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.ShieldUpgrade);
        currentMegaCoinLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.MegaCoinUpgrade);
        currentStaminaLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.StaminaUpgrade);

    }

    public List<Upgrades> GetAvaliablePowerUps()
    {
        List<Upgrades> avaliableUpgrades = new List<Upgrades>();

        if (currentChargeLevel > 0)
        {
            avaliableUpgrades.Add(Upgrades.ChargeUpgrade);
        }

        if (currentShieldLevel > 0)
        {
            avaliableUpgrades.Add(Upgrades.ShieldUpgrade);
        }

        if (currentMegaCoinLevel > 0)
        {
            avaliableUpgrades.Add(Upgrades.MegaCoinUpgrade);
        }

        return avaliableUpgrades;
    }

    public void ActivatePowerUp(CollectableType _collectableType)
    {
        switch (_collectableType)
        {

            case CollectableType.Charge:
                ChargeCollected();
                break;

            case CollectableType.Shield:
                ShieldCollected();
                break;

            case CollectableType.Stamina:
                StaminaCollected();
                character.RestoreChargePower(10f);
                break;

            default:
                Debug.LogError("Unknown Collectable in ActivatePowerUp");
                break;
        }

    }

    public void StaminaCollected()
    {
        switch (currentStaminaLevel)
        {
            case 0:
                character.RestoreChargePower(0f);
                break;

            case 1:
                character.RestoreChargePower(10f);
                break;

            case 2:
                character.RestoreChargePower(15f);
                break;

            case 3:
                character.RestoreChargePower(20f);
                break;

            case 4:
                character.RestoreChargePower(25f);
                break;

            default:
                Debug.LogError("Invalid level in stamina Collected");
                break;
        }
    }

    public void ChargeCollected()
    {
        switch (currentChargeLevel)
        {
            case 0:
                character.ActivateUnlimitedCharge(0f);
                break;

            case 1:
                character.ActivateUnlimitedCharge(6f);
                break;

            case 2:
                character.ActivateUnlimitedCharge(7f);
                break;

            case 3:
                character.ActivateUnlimitedCharge(8f);
                break;

            case 4:
                character.ActivateUnlimitedCharge(9f);
                break;

            default:
                Debug.LogError("Invalid level in Charge Collected");
                break;
        }
    }

    public void ShieldCollected()
    {
        switch (currentShieldLevel)
        {
            case 0:
                character.ActivateShield(0f);
                break;

            case 1:
                character.ActivateShield(5f);
                break;

            case 2:
                character.ActivateShield(6f);
                break;

            case 3:
                character.ActivateShield(7f);
                break;

            case 4:
                character.ActivateShield(8f);
                break;

            default:
                Debug.LogError("Invalid level in Shield Collected");
                break;
        }
    }
    
}
