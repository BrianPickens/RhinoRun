using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChargeTutorial : PopUpBase
{
    [SerializeField]
    private GameObject left = null;

    [SerializeField]
    private GameObject right = null;

    [SerializeField]
    private GameObject text = null;

    [SerializeField]
    private Image charge = null;

    [SerializeField]
    private Sprite chargeOnSprite = null;

    [SerializeField]
    private Animator chargeAnimator = null;

    public Action OnSectionCompleted;

    public void ChargeClicked()
    {
        left.SetActive(false);
        right.SetActive(false);
        text.SetActive(false);
        charge.sprite = chargeOnSprite;
        chargeAnimator.Rebind();
        chargeAnimator.speed = 0;
        SectionCompleted();
    }

    private void SectionCompleted()
    {
        if (OnSectionCompleted != null)
        {
            OnSectionCompleted();
        }
    }
}
