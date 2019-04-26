using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowTutorial : PopUpBase
{

    public Action OnSectionCompleted;

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
