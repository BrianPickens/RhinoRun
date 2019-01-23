using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPooler : CollectablePool
{
    protected override void Start()
    {
        base.Start();
        UpdateCoinTexture();
    }

    private void UpdateCoinTexture()
    {

        int coinLevel = SaveManager.Instance.GetUpgradeLevel(Upgrades.CoinsUpgrade);
        for (int i = 0; i < pooledCollectables.Count; i++)
        {
            pooledCollectables[i].UpdateTexture(coinLevel);
        }
    }
}
