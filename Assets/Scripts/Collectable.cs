using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CollectableType { None, Gold, Stamina, Charge, Shield, MegaCoin }
public class Collectable : MonoBehaviour
{
    [SerializeField]
    private CollectableType myCollectableType;
    public CollectableType MyCollectableType
    {
        get { return myCollectableType; }
    }

    [SerializeField]
    private Animator myAnimator;

    private Transform poolTransform;

    public Action<CollectableType> OnCollectForManager;
    public Action<Collectable> OnCollectForLevelBlock;

    public virtual void UpdateTexture(int _level)
    {
        //make abstract?
    }

    public void Initialize(Action<CollectableType> _collectableCallbackToManager, Action<Collectable> _collectableCallbackToLevelBlock, float _animationOffset)
    {
        gameObject.SetActive(true);
        myAnimator.SetFloat("Offset", _animationOffset);
        OnCollectForManager = null;
        OnCollectForManager += _collectableCallbackToManager;
        OnCollectForLevelBlock = null;
        OnCollectForLevelBlock += _collectableCallbackToLevelBlock;

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
        if (OnCollectForManager != null)
        {
            OnCollectForManager(myCollectableType);
        }

        if (OnCollectForLevelBlock != null)
        {
            OnCollectForLevelBlock(this);
        }

        Recycle();
    }
}
