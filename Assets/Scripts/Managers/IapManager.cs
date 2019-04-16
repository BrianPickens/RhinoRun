using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;


public class IapManager : MonoBehaviour
{

    public Action OnPurchaseCompleted;
    public Action OnPurchasedFailed;

    public void OnPurchaseComplete(Product _product)
    {

        string productID = _product.definition.id;

        //Debug.Log("product id is: " + productID);
        switch (productID)
        {
            case "remove_ads":
                if (SaveManager.Instance != null)
                {
                    SaveManager.Instance.SetHasRemoveAds(true);
                }
                break;

            default:
                Debug.LogError("unknown product in on purchase complete");
                break;

        }

        if (OnPurchaseCompleted != null)
        {
            OnPurchaseCompleted();
        }

    }

    public void OnPurchaseFail(Product _product, PurchaseFailureReason _reason)
    {
        if (OnPurchasedFailed != null)
        {
            OnPurchasedFailed();
        }
        Debug.Log("Purchase failed");
    }

}
