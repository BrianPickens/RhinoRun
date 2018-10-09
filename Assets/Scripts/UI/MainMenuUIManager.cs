using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuUIManager : MonoBehaviour
{

    public Action OnPlayPress;
    public Action OnUpgradesPress;

    public void PlayPress()
    {
        if (OnPlayPress != null)
        {
            OnPlayPress();
        }
    }

    public void SettingsPress()
    {

    }

    public void UpgradesPress()
    {
        if (OnUpgradesPress != null)
        {
            OnUpgradesPress();
        }
    }

}
