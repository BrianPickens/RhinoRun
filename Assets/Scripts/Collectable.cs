using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CollectableType { Gold }
public class Collectable : MonoBehaviour
{
    [SerializeField]
    private CollectableType myCollectableType;
    public CollectableType MyCollectableType
    {
        get { return myCollectableType; }
    }

    public Action<CollectableType> OnCollect;

    public void Initialize(Action<CollectableType> _collectableCallback)
    {
        gameObject.SetActive(true);
        OnCollect = null;
        OnCollect += _collectableCallback;
    }

    public void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (OnCollect != null)
        {
            OnCollect(myCollectableType);
        }
        gameObject.SetActive(false);
    }
}
