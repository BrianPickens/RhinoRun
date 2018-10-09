using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{
    public static InitializationManager instance = null;

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private SceneLoadingManager sceneLoadingManager;

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
        StartCoroutine(WaitForInitialization());
    }

    private IEnumerator WaitForInitialization()
    {
        while (!soundManager.Initialized && !sceneLoadingManager.Initialized)
        {
            yield return null;
        }
        Debug.Log("Managers Initialized");
        sceneLoadingManager.LoadScene("MainMenu");
    }


}
