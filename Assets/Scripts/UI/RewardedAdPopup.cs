using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RewardedAdPopup : MonoBehaviour
{

    [SerializeField]
    private Animator myAnimator;

    public Action<bool> OnRewardedAdResponse;

    public void OpenRewardedAdPopup()
    {
        gameObject.SetActive(true);
    }
    
    //closed by animator
    public void CloseRewardedAdPopup()
    {
        gameObject.SetActive(false);
    }

    public void RewardedAdResponse(bool _response)
    {
        if (OnRewardedAdResponse != null)
        {
            OnRewardedAdResponse(_response);
        }

        myAnimator.SetTrigger("Exit");

    }

}
