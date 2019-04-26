using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]
    private GameUI gameUI = null;

    [SerializeField]
    private ArrowTutorial arrowTutorial = null;

    [SerializeField]
    private DoubleArrowTutorial doubleArrowTutorial = null;

    [SerializeField]
    private ChargeTutorial chargeTutorial = null;

    [SerializeField]
    private StaminaTutorial staminaTutorial = null;

    public void Start()
    {
        arrowTutorial.OnSectionCompleted = null;
        arrowTutorial.OnSectionCompleted += ArrowTutorialCompleted;

        doubleArrowTutorial.OnSectionCompleted = null;
        doubleArrowTutorial.OnSectionCompleted += DoubleArrowTutorialCompleted;

        chargeTutorial.OnSectionCompleted = null;
        chargeTutorial.OnSectionCompleted += ChargeTutorialCompleted;

        staminaTutorial.OnSectionCompleted = null;
        staminaTutorial.OnSectionCompleted += StaminaTutorialCompleted;
    }

    public void DisplayArrowTutorial()
    {
        arrowTutorial.OpenPopUp();
        PauseTime();
    }

    public void DisplayDoubleArrowTutorial()
    {
        doubleArrowTutorial.OpenPopUp();
        PauseTime();
    }

    public void DisplayChargeTutorial()
    {
        chargeTutorial.OpenPopUp();
        PauseTime();
    }

    public void DisplayStaminaTutorial()
    {
        staminaTutorial.OpenPopUp();
        PauseTime();
    }

    public void SetTutorialCompleted()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SetTutorialCompleted();
        }
    }

    public void ArrowTutorialCompleted()
    {
        gameUI.ShowArrows();
        UnpauseTime();
    }

    public void DoubleArrowTutorialCompleted()
    {
        gameUI.ShowDoubleArrows();
        UnpauseTime();
    }

    public void ChargeTutorialCompleted()
    {
        gameUI.ShowCharge();
        UnpauseTime();
    }

    public void StaminaTutorialCompleted()
    {
        UnpauseTime();
    }

    private void PauseTime()
    {
        Time.timeScale = 0;
    }

    private void UnpauseTime()
    {
        Time.timeScale = 1;
    }

}
