using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{
    private static InitializationManager instance = null;
    public static InitializationManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private SceneLoadingManager sceneLoadingManager;

    [SerializeField]
    private SaveManager saveManager;

    [SerializeField]
    private LoadingTransition loadingScreen;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitializeManagers();   
    }

    private void InitializeManagers()
    {
        soundManager.Initialize();
        sceneLoadingManager.Initialize();
        saveManager.Initialize();
        StartCoroutine(WaitForInitialization());
    }

    private IEnumerator WaitForInitialization()
    {
        while (!soundManager.Initialized && !sceneLoadingManager.Initialized && !saveManager.Initialized)
        {
            yield return null;
        }
        Debug.Log("Managers Initialized");
        loadingScreen.ShowLoading();
        yield return new WaitForSeconds(1f);
        sceneLoadingManager.LoadScene("MainMenu");
    }


}
