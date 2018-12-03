using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingDisplayUI : MonoBehaviour
{

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private Text endingPointsText;

    [SerializeField]
    private Text endingDistanceText;

    private int finalPoints;

    private int finalDistance;

    public void OpenEndingDisplay(int _points, int _distance)
    {
        finalPoints = _points;
        finalDistance = _distance;
        gameObject.SetActive(true);
        myAnimator.SetTrigger("Open");
    }

    public void StartEndingSequence()
    {
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        yield return null;
        endingPointsText.text = "" + finalPoints;
        endingDistanceText.text = "" + finalDistance + " Meters";
    }

}
