using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingDisplayUI : MonoBehaviour
{

    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private Text endingPointsText = null;

    [SerializeField]
    private Text endingDistanceText = null;

    [SerializeField]
    private GameObject metersDisplay = null;

    [SerializeField]
    private GameObject coinsDisplay = null;

    [SerializeField]
    private GameObject multiplierButton = null;

    [SerializeField]
    private GameObject buttonsPanel = null;

    [SerializeField]
    private GameObject bestDisplay = null;

    [SerializeField]
    private AudioClip clappingSounds = null;

    [SerializeField]
    private AudioClip popSound = null;

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
        PlaySound(clappingSounds);
        yield return new WaitForSeconds(0.5f);
        float currentDistance = 0f;
        float t = 0f;

        metersDisplay.SetActive(true);
        while (currentDistance < finalDistance)
        {
            currentDistance = Mathf.Lerp(0, finalDistance, t);
            currentDistance = Mathf.Round(currentDistance);
            string currentDistanceString = currentDistance.ToString("#,#");
            endingDistanceText.text = currentDistanceString;
            t += Time.deltaTime * 0.8f;
            yield return null;
        }

        string finalDistanceString = finalDistance.ToString("#,#");
        endingDistanceText.text = finalDistanceString;

        if (bestDistance)
        {
            PlaySound(popSound);
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
            string currentPointsString = currentPoints.ToString("#,#");
            endingPointsText.text = currentPointsString;
            t += Time.deltaTime * 0.8f;
            yield return null;
        }
        string endingPointsString = finalPoints.ToString("#,#");
        endingPointsText.text = endingPointsString;

        PlaySound(popSound);
        multiplierButton.SetActive(true);

        yield return new WaitForSeconds(0.25f);
        PlaySound(popSound);
        buttonsPanel.SetActive(true);
    }

    private void PlaySound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
        }
    }

}
