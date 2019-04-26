using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaminaTutorial : PopUpBase
{
    public Action OnSectionCompleted;

    [SerializeField]
    private GameObject popUpOne = null;

    [SerializeField]
    private GameObject popUpTwo = null;

    public void ShowPopUpTwo()
    {
        popUpOne.SetActive(false);
        popUpTwo.SetActive(true);
    }

    public override void ExitPopUp()
    {
        SectionCompleted();
        base.ExitPopUp();
    }

    private void SectionCompleted()
    {
        if (OnSectionCompleted != null)
        {
            OnSectionCompleted();
        }
    }
}
