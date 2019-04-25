using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{

    [SerializeField]
    private Sprite leftSprite = null;

    [SerializeField]
    private Sprite leftPressedSprite= null;

    [SerializeField]
    private Sprite rightSprite = null;

    [SerializeField]
    private Sprite rightPressedSprite = null;

    [SerializeField]
    private Sprite doubleLeftSprite = null;

    [SerializeField]
    private Sprite doubleLeftPressedSprite = null;

    [SerializeField]
    private Sprite doubleRightSprite = null;

    [SerializeField]
    private Sprite doubleRightPressedSprite = null;

    [SerializeField]
    private Image leftButton = null;

    [SerializeField]
    private Image rightButton = null;

    [SerializeField]
    private Image doubleLeftButton = null;

    [SerializeField]
    private Image doubleRightButton = null;


    public void LeftPressed()
    {
        leftButton.sprite = leftPressedSprite;
    }

    public void LeftPressFinished()
    {
        leftButton.sprite = leftSprite;
    }

    public void RightPressed()
    {
        rightButton.sprite = rightPressedSprite;
    }

    public void RightPressFinished()
    {
        rightButton.sprite = rightSprite;
    }

    public void DoubleLeftPressed()
    {
        doubleLeftButton.sprite = doubleLeftPressedSprite;
    }

    public void DoubleLeftPressFinished()
    {
        doubleLeftButton.sprite = doubleLeftSprite;
    }

    public void DoubleRightPressed()
    {
        doubleRightButton.sprite = doubleRightPressedSprite;
    }

    public void DoubleRightPressedFinished()
    {
        doubleRightButton.sprite = doubleRightSprite;
    }

}
