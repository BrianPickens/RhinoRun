using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePurchaseFailedPopUp : PopUpBase
{

    [SerializeField]
    private Text failureReasonText = null;

    public void SetFailureReason(string _reason)
    {
        failureReasonText.text = _reason;
    }

}
