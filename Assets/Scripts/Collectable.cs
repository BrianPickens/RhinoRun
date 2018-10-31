using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CollectableType { None, Gold, Stamina, PowerUp }
public class Collectable : MonoBehaviour
{
    [SerializeField]
    private CollectableType myCollectableType;
    public CollectableType MyCollectableType
    {
        get { return myCollectableType; }
    }

    private Transform poolTransform;

    public Action<CollectableType> OnCollect;

    public void Initialize(Action<CollectableType> _collectableCallback)
    {
        gameObject.SetActive(true);
        OnCollect = null;
        OnCollect += _collectableCallback;

    }

    public void SetLocation(Transform _parent)
    {
        poolTransform = transform.parent;
        gameObject.transform.SetParent(_parent);
        gameObject.transform.localPosition = Vector3.zero;
    }

    public void Recycle()
    {
        gameObject.transform.SetParent(poolTransform);
        gameObject.SetActive(false);
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
