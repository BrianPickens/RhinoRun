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
    private Image leftButton = null;

    [SerializeField]
    private Image rightButton = null;

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

}
