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

    [SerializeField]
    private ObjectPooler staminaBars;

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

        GameObject obj = null;

        switch (_type)
        {
            case CollectableType.Gold:
                obj = goldCoins.GetPooledObject();
                break;
            case CollectableType.Stamina:
                obj = staminaBars.GetPooledObject();
                break;

            case CollectableType.PowerUp:

                break;

            default:
                Debug.LogError("Invalid Request to Collectable Pooler");
                break;
        }
        return obj;
    }



}
