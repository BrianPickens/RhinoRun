using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private MainMenuUIManager mainUI;

    private void Start()
    {
        Init();   
    }

    private void Init()
    {
        mainUI.OnPlayPress = null;
        mainUI.OnPlayPress += PlayGame;

        mainUI.OnUpgradesPress = null;
        mainUI.OnUpgradesPress += OpenUpgrades;
    }

    private void PlayGame()
    {
        SceneLoadingManager.instance.LoadScene("PlayScene");
    }

    private void OpenUpgrades()
    {
        SceneLoadingManager.instance.LoadScene("Upgrades");
    }

}
