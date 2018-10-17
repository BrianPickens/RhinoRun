using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradesUIManager : MonoBehaviour
{

    public Action OnMenuPress;


    public void MenuButtonPress()
    {
        if (OnMenuPress != null)
        {
            OnMenuPress();
        }
    }
}
