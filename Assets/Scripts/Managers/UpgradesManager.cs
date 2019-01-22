using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//upgrade types
//increase coin amount
//unlimited charge
//rhino snax boost
//shield
//mega coin
//powerup drpo frequency


//pick up types
//coins
//unlimited cgarge
//rhino snacks
//Shield
//mega coin

public class UpgradesManager : MonoBehaviour
{

    [SerializeField]
    private UpgradesUIManager upgradesUI;

    private void Start()
    {
        Init();   
    }

    private void Init()
    {
        if (InitializationManager.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Initialization");
        }

        upgradesUI.Init();

        upgradesUI.OnMenuPress = null;
        upgradesUI.OnMenuPress += ReturnToMain;
    }

    public void ReturnToMain()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("MainMenu");
        }
    }

}
