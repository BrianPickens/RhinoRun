﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour {

    private static SceneLoadingManager instance = null;
    public static SceneLoadingManager Instance
    {
        get { return instance; }
    }

    private bool initialized;
    public bool Initialized
    {
        get { return initialized; }
    }

    public void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        initialized = true;
    }

    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadSceneAsync(string _sceneName)
    {
        StartCoroutine(StartAsyncLoad(_sceneName));
    }

    private IEnumerator StartAsyncLoad(string _sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
