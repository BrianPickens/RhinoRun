using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IapManager : MonoBehaviour
{

    public void OnPurchaseComplete(Product _product)
    {
        //remember to save locally and to cloud, then make sure on load make sure that remove ads is always given no matter which save is better

        //switch (_purchaseID)
        //{
        //    case "removeAds":
        //        //remove ads
        //        break;

        //    default:
        //        Debug.LogError("unknown purchase id from iap button");
        //        break;
        //}

    }

    public void OnPurchaseFailed(Product _product, PurchaseFailureReason _reason)
    {
        Debug.Log("Purchase failed");
    }

}
