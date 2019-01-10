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
    private CollectablePool goldCoins;

    [SerializeField]
    private CollectablePool staminaBars;

    [SerializeField]
    private CollectablePool shields;

    [SerializeField]
    private CollectablePool boosts;

    [SerializeField]
    private CollectablePool megaCoins;

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


    public Collectable GetCollectable(CollectableType _type)
    {

        Collectable collectable = null;

        switch (_type)
        {
            case CollectableType.Gold:
                collectable = goldCoins.GetPooledCollectable();
                break;
            case CollectableType.Stamina:
                collectable = staminaBars.GetPooledCollectable();
                break;

            case CollectableType.Shield:
                collectable = shields.GetPooledCollectable();
                break;

            case CollectableType.Boost:
                collectable = boosts.GetPooledCollectable();
                break;

            case CollectableType.MegaCoin:
                collectable = megaCoins.GetPooledCollectable();
                break;

            default:
                Debug.LogError("Invalid Request to Collectable Pooler");
                break;
        }
        return collectable;
    }



}
