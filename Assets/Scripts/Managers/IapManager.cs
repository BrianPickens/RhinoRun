using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IapManager : MonoBehaviour
{

    public void OnPurchaseComplete(string _purchaseID)
    {
        switch (_purchaseID)
        {
            case "removeAds":
                //remove ads
                break;

            default:
                Debug.LogError("unknown purchase id from iap button");
                break;
        }
    }

    public void OnPurchaseFailed(string _error)
    {
        Debug.Log("Purchase failed");
    }

}
