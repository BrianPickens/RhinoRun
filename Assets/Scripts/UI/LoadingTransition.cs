using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTransition : MonoBehaviour {

    [SerializeField]
    private Animator myAnimator;

    public void ShowLoading()
    {
        myAnimator.SetTrigger("MoveIn");
    }

    public void HideLoading()
    {
        myAnimator.SetTrigger("MoveOut");
    }

    public void StartWithLoading()
    {
        myAnimator.SetTrigger("StartDown");
    }

}
