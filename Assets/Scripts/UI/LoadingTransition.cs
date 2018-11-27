using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTransition : MonoBehaviour {

    [SerializeField]
    private Animator myAnimator;

    public void ShowLoading()
    {
        myAnimator.SetBool("Loading", true);
    }

    public void HideLoading()
    {
        myAnimator.SetBool("Loading", false);
    }

}
