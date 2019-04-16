using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpBase : MonoBehaviour
{

    public UnityEvent myEvent;

    [SerializeField]
    private Animator myAnimator = null;

    public void OpenPopUp()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }

    public void ExitPopUp()
    {
        if (myAnimator != null)
        {
            myAnimator.SetTrigger("Exit");
        }
        else
        {
            ActivateEvent();
        }
    }

    public void ActivateEvent()
    {
        myEvent.Invoke();
    }
}
