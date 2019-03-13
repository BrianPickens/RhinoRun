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
    private GameObject metersDisplay;

    [SerializeField]
    private GameObject coinsDisplay;

    [SerializeField]
    private GameObject multiplierButton;

    [SerializeField]
    private GameObject buttonsPanel;

    [SerializeField]
    private GameObject bestDisplay;

    private int finalPoints;

    private int finalDistance;

    private bool bestDistance;

    public void OpenEndingDisplay(int _points, int _distance, bool _isHighscore)
    {
        finalPoints = _points;
        finalDistance = _distance;
        bestDistance = _isHighscore;
        gameObject.SetActive(true);
        myAnimator.SetTrigger("Open");
    }

    public void DisableMultiplierButton()
    {
        multiplierButton.GetComponent<Button>().interactable = false;
    }

    public void StartEndingSequence()
    {
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {

        yield return new WaitForSeconds(0.5f);
        float currentDistance = 0f;
        float t = 0f;

        metersDisplay.SetActive(true);
        while (currentDistance < finalDistance)
        {
            currentDistance = Mathf.Lerp(0, finalDistance, t);
            currentDistance = Mathf.Round(currentDistance);
            endingDistanceText.text = "" + currentDistance;
            t += Time.deltaTime * 0.8f;
            yield return null;
        }
        endingDistanceText.text = "" + finalDistance;

        if (bestDistance)
        {
            bestDisplay.SetActive(true);
        }

        yield return new WaitForSeconds(0.25f);
        float currentPoints = 0f;
        t = 0f;
        coinsDisplay.SetActive(true);
        while (currentPoints < finalPoints)
        {
            currentPoints = Mathf.Lerp(0, finalPoints, t);
            currentPoints = Mathf.Round(currentPoints);
            endingPointsText.text = "" + currentPoints;
            t += Time.deltaTime * 0.8f;
            yield return null;
        }

        endingPointsText.text = "" + finalPoints;

        multiplierButton.SetActive(true);

        yield return new WaitForSeconds(0.25f);
        buttonsPanel.SetActive(true);
    }

}
