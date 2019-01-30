using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimer : MonoBehaviour
{
    [SerializeField]
    private Image meter;

    public void DisplayTimer()
    {
        meter.fillAmount = 1f;
        gameObject.SetActive(true);
    }

    public void UpdateTimer(float _fillAmount)
    {
        meter.fillAmount = _fillAmount;
    }

    public void EndTimer()
    {
        gameObject.SetActive(false);
    }

}
