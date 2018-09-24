﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [SerializeField]
    private GameObject pooledObject;

    [SerializeField]
    List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        
        GameObject newObj = (GameObject)Instantiate(pooledObject);
        pooledObjects.Add(newObj);
        newObj.transform.SetParent(gameObject.transform);
        return newObj;
    }

}