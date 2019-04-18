using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpBase : MonoBehaviour
{

    public UnityEvent myEvent;

    [SerializeField]
    private Animator myAnimator = null;

    public virtual void OpenPopUp()
    {
        gameObject.SetActive(true);
    }

    public virtual void ClosePopUp()
    {
        gameObject.SetActive(false);
    }

    public virtual void ExitPopUp()
    {
        //make sure to add the activate event animation event in the animation
        if (myAnimator != null)
        {
            myAnimator.SetTrigger("Exit");
        }
        else
        {
            ActivateEvent();
        }
    }

    public virtual void ActivateEvent()
    {
        myEvent.Invoke();
    }
}
