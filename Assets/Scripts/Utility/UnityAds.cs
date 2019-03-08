using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public enum AdType { Normal, Rewarded };
public static class UnityAds
{

    private static string adString = "video";

    private static string rewardedAdString = "rewardedVideo";

    private static AdType currentAdType;

    private static Action<AdType,bool> OnAdCompleted;

    public static bool CheckForAd()
    {
        if (Advertisement.IsReady(adString))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ShowAd(Action<AdType, bool> _callback)
    {
        OnAdCompleted = null;
        OnAdCompleted += _callback;
        currentAdType = AdType.Normal;
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCompleted;
        Advertisement.Show(adString, options);
    }

    public static bool CheckForRewardedAd()
    {
        if (Advertisement.IsReady(rewardedAdString))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ShowRewardedAd(Action<AdType, bool> _callback)
    {
        OnAdCompleted = null;
        OnAdCompleted += _callback;
        currentAdType = AdType.Rewarded;
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCompleted;
        Advertisement.Show(rewardedAdString, options);
    }

    private static void AdCompleted(ShowResult _result)
    {

        bool adCompleted = false;

        switch (_result)
        {
            case ShowResult.Finished:
                adCompleted = true;
                break;

            case ShowResult.Skipped:
                adCompleted = false;
                break;

            case ShowResult.Failed:
                Debug.Log("Ad failed to show");
                adCompleted = false;
                break;
        }

        if (OnAdCompleted != null)
        {
            OnAdCompleted(currentAdType, adCompleted);
        }

        OnAdCompleted = null;
    }

}
