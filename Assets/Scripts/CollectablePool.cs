using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePool : MonoBehaviour
{

    [SerializeField]
    private Collectable pooledCollectable;

    protected List<Collectable> pooledCollectables = new List<Collectable>();

    [SerializeField]
    private int startAmount;

    protected virtual void Start()
    {
        for (int i = 0; i < startAmount; i++)
        {
            Collectable newCollectable = (Collectable)Instantiate(pooledCollectable);
            pooledCollectables.Add(newCollectable);
            newCollectable.transform.SetParent(gameObject.transform);
        }
    }

    public Collectable GetPooledCollectable()
    {
        for (int i = 0; i < pooledCollectables.Count; i++)
        {
            if (!pooledCollectables[i].gameObject.activeInHierarchy)
            {
                return pooledCollectables[i];
            }
        }

        Collectable newCollectable = (Collectable)Instantiate(pooledCollectable);
        pooledCollectables.Add(newCollectable);
        newCollectable.transform.SetParent(gameObject.transform);
        return newCollectable;
    }

}
