using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType { None, Barrier, Breakable };
public class ObstacleBlock : MonoBehaviour {

    [SerializeField]
    private Transform myTransform = null;
    [SerializeField]
    private ObstacleType myObstacleType = ObstacleType.None;
    public ObstacleType MyObstacleType
    {
        get { return myObstacleType; }
    }
    [SerializeField]
    private Collider myCollider = null;

    [SerializeField]
    private GameObject brokenObject = null;
    [SerializeField]
    private GameObject fixedObject = null;

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
