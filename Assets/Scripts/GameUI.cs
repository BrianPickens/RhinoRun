using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    private int points;

    [SerializeField]
    private Text pointsDisplay;

    public void AddPoints(int _points)
    {
        points += _points;
        pointsDisplay.text = "" + points;
    }

}
