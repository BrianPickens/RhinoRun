using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdResult : MonoBehaviour
{

    [SerializeField]
    private Text infoText;

    [SerializeField]
    private Animator myAnimator;

    public void SetInfo(string _text)
    {
        infoText.text = _text;

        gameObject.SetActive(true);
    }

    public void CloseRewardedAdResult()
    {
        gameObject.SetActive(false);
    }

    public void AcceptButton()
    {
        myAnimator.SetTrigger("Exit");
    }
}
