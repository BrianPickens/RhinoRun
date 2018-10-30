using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePooler : MonoBehaviour
{

    private static CollectablePooler instance;
    public static CollectablePooler Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private ObjectPooler goldCoins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public GameObject GetCollectable(CollectableType _type)
    {
        GameObject obj = goldCoins.GetPooledObject();
        return obj;
    }

    public GameObject GetGoldCoin()
    {
        GameObject obj = goldCoins.GetPooledObject();
        return obj;

    }


}
