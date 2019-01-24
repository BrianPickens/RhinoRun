using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private int maxLevels;

    [SerializeField]
    private Text currentLevelInfo;

    [SerializeField]
    private Text nextLevelInfo;

    [SerializeField]
    private Sprite unlockedBackground;

    [SerializeField]
    private List<Image> upgradeLevels = new List<Image>();

    [SerializeField]
    [TextArea]
    private List<string> levelInfo = new List<string>();

    public void SetUpgradeInfo(int _currentLevel)
    {
        SetUpgradeText(_currentLevel);
    }

    private void SetUpgradeText(int _currentLevel)
    {
        currentLevelInfo.text = "" + levelInfo[_currentLevel];
        if (_currentLevel < 4)
        {
            nextLevelInfo.text = "" + levelInfo[_currentLevel + 1];
        }
        else
        {
            nextLevelInfo.text = "--";
        }
    }

    private void SetUpgradeLevelImages(int _currentLevel)
    {
        for (int i = 0; i < _currentLevel; i++)
        {
            upgradeLevels[i].sprite = unlockedBackground;
        }

        for (int i = 0; i < upgradeLevels.Count; i++)
        {
            if (i >= maxLevels)
            {
                upgradeLevels[i].gameObject.SetActive(false);
            }
        }
    }
}
