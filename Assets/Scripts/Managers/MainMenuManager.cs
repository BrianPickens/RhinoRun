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

        if (InitializationManager.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Initialization");
        }

        mainUI.OnPlayPress = null;
        mainUI.OnPlayPress += PlayGame;

        mainUI.OnUpgradesPress = null;
        mainUI.OnUpgradesPress += OpenUpgrades;

        InitializeUI();

    }

    private void InitializeUI()
    {
        if (SaveManager.Instance != null)
        {
            mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
            mainUI.UpdateScoreDisplay(SaveManager.Instance.GetCurrentHighscore());
        }
    }

    private void PlayGame()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("PlayScene");
        }
    }

    private void OpenUpgrades()
    {
        if (SceneLoadingManager.Instance != null)
        {
            SceneLoadingManager.Instance.LoadScene("Upgrades");
        }
    }

    //debug options
    public void ResetMoney()
    {
        SaveManager.Instance.ResetMoney();
        mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
    }

    public void ResetScore()
    {
        SaveManager.Instance.ResetScore();
        mainUI.UpdateScoreDisplay(SaveManager.Instance.GetCurrentHighscore());
    }

    public void AddMoney()
    {
        SaveManager.Instance.UpdateCoins(500);
        mainUI.UpdateCoinsDisplay(SaveManager.Instance.GetCurrentCoins());
    }
    //end debug options

}
