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
    private CollectablePool goldCoins = null;

    [SerializeField]
    private CollectablePool staminaBars = null;

    [SerializeField]
    private CollectablePool charges = null;

    [SerializeField]
    private CollectablePool shields = null;

    [SerializeField]
    private CollectablePool megaCoins = null;

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

            case CollectableType.Charge:
                collectable = charges.GetPooledCollectable();
                break;

            case CollectableType.Shield:
                collectable = shields.GetPooledCollectable();
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
