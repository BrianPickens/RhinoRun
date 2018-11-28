using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradesUIManager : MonoBehaviour
{

    [SerializeField]
    private LoadingTransition loadingScreen;

    public Action OnMenuPress;

    public void Init()
    {
        loadingScreen.StartWithLoading();
        loadingScreen.HideLoading();
    }

    public void MenuButtonPress()
    {
        loadingScreen.ShowLoading();
        if (OnMenuPress != null)
        {
            StartCoroutine(SceneLoadDelay(OnMenuPress));
        }
    }

    private IEnumerator SceneLoadDelay(Action _callback)
    {
        yield return new WaitForSeconds(1f);
        _callback();
    }
}
