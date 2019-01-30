using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private GameUI gameUI;

    private int currentChargeLevel;
    private int currentShieldLevel;
    private int currentMegaCoinLevel;
    private int currentStaminaLevel;

    private bool chargeActive;
    private bool shieldActive;
    private float chargeTimer;
    private float shieldTimer;
    private float chargeTimerRemaining;
    private float shieldTimerRemaining;

    public void Initialize()
    {
        SetCurrentPowerUpLevels();
    }

    private void Update()
    {
        if (chargeActive)
        {
            chargeTimerRemaining -= Time.deltaTime;
            gameUI.UpdateUnlimitedChargeTimer(chargeTimerRemaining / chargeTimer);
            if (chargeTimerRemaining <= 0)
            {
                chargeActive = false;
                gameUI.HideUnlimitedChargeTimer();
                character.DeactivateUnlimitedCharge();
            }
        }

        if (shieldActive)
        {
            shieldTimerRemaining -= Time.deltaTime;
            gameUI.UpdateShieldTimer(shieldTimerRemaining / shieldTimer);
            if (shieldTimerRemaining <= 0)
            {
                shieldActive = false;
                gameUI.HideShieldTimer();
                character.DeactivateShield();
            }
        }
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
        float duration = 0f;

        switch (currentChargeLevel)
        {
            case 0:
                character.ActivateUnlimitedCharge();
                duration = 0f;
                break;

            case 1:
                character.ActivateUnlimitedCharge();
                duration = 5f;
                break;

            case 2:
                character.ActivateUnlimitedCharge();
                duration = 6f;
                break;

            case 3:
                character.ActivateUnlimitedCharge();
                duration = 7f;
                break;

            case 4:
                character.ActivateUnlimitedCharge();
                duration = 8f;
                break;

            default:
                Debug.LogError("Invalid level in Charge Collected");
                break;
        }

        character.ActivateUnlimitedCharge();
        gameUI.DisplayUnlimitedChargeTimer();
        chargeActive = true;
        chargeTimer = duration;
        chargeTimerRemaining = duration;
    }

    public void ShieldCollected()
    {
        float duration = 0f;

        switch (currentShieldLevel)
        {
            case 0:
                duration = 0f;
                break;

            case 1:
                duration = 5f;
                break;

            case 2:
                duration = 6f;
                break;

            case 3:
                duration = 7f;
                break;

            case 4:
                duration = 8f;
                break;

            default:
                Debug.LogError("Invalid level in Shield Collected");
                break;
        }

        character.ActivateShield();
        gameUI.DisplayShieldTimer();
        shieldActive = true;
        shieldTimer = duration;
        shieldTimerRemaining = duration;

    }
    
}
