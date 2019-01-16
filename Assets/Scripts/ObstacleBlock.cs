using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType { None, Barrier, Breakable };
public class ObstacleBlock : MonoBehaviour {

    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private ObstacleType myObstacleType;
    public ObstacleType MyObstacleType
    {
        get { return myObstacleType; }
    }
    [SerializeField]
    private Collider myCollider;

    [SerializeField]
    private GameObject brokenObject;
    [SerializeField]
    private GameObject fixedObject;

    private Transform poolTransform;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void SetLocation(Transform _parent)
    {
        poolTransform = myTransform.parent;
        myTransform.SetParent(_parent);
        myTransform.localPosition = Vector3.zero;
    }

    public void Activate()
    {
        myCollider.enabled = true;
    }

    public void Recycle()
    {
        if (myObstacleType == ObstacleType.Breakable)
        {
            fixedObject.SetActive(true);
            brokenObject.SetActive(false);
        }
        myTransform.SetParent(poolTransform);
        gameObject.SetActive(false);
    }

    public void Destroyed()
    {
        myCollider.enabled = false;
        if (myObstacleType == ObstacleType.Breakable)
        {
            fixedObject.SetActive(false);
            brokenObject.SetActive(true);
        }
    }




}
