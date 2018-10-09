using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingDisplayUI : MonoBehaviour
{

    [SerializeField]
    private Text endingPointsText;

    [SerializeField]
    private Text endingDistanceText;


    public void DisplayEnding(int _points, int _distance)
    {
        endingPointsText.text = "" + _points + " Money";
        endingDistanceText.text = "" + _distance + " Meters";
        gameObject.SetActive(true);
    }

}
