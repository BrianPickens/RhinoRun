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

    [SerializeField]
    private GameObject buttonsPanel;

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

        yield return new WaitForSeconds(1f);
        float currentDistance = 0f;
        float t = 0f;

        while (currentDistance < finalDistance)
        {
            currentDistance = Mathf.Lerp(0, finalDistance, t);
            currentDistance = Mathf.Round(currentDistance);
            endingDistanceText.text = "" + currentDistance + " Meters";
            t += Time.deltaTime * 0.5f;
            yield return null;
        }
        endingDistanceText.text = "" + finalDistance + " Meters";

        yield return new WaitForSeconds(1f);
        float currentPoints = 0f;
        t = 0f;

        while (currentPoints < finalPoints)
        {
            currentPoints = Mathf.Lerp(0, finalPoints, t);
            currentPoints = Mathf.Round(currentPoints);
            endingPointsText.text = "" + currentPoints;
            t += Time.deltaTime * 0.5f;
            yield return null;
        }
        endingPointsText.text = "" + finalPoints;

    }

}
