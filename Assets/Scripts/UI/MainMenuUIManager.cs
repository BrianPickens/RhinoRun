using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    [SerializeField]
    private Text coinsText;

    [SerializeField]
    private Text scoreText;

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

    public void UpdateCoinsDisplay(int _coins)
    {
        coinsText.text = "" + _coins;
    }

    public void UpdateScoreDisplay(int _score)
    {
        scoreText.text = _score + " Meters!";
    }

}
