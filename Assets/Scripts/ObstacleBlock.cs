using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType { None, Barrier, Breakable };
public class ObstacleBlock : MonoBehaviour {

    private Vector3 previousPos;
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private ObstacleType myObstacleType;
    public ObstacleType MyObstacleType
    {
        get { return myObstacleType; }
    }
    private Collider myCollider;
    private bool checkForCollision;

    private void Start()
    {
        previousPos = myTransform.position;
    }

    private void FixedUpdate()
    {
        if (checkForCollision)
        {
            Vector3 movement = myTransform.position - previousPos;


            float movementMagnitude = movement.magnitude;
            RaycastHit hitInfo;

            if (Physics.Raycast(previousPos, movement, out hitInfo, movementMagnitude, 1 << 9))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    hitInfo.collider.GetComponent<Character>().HandleCollision(this);
                }
            }

            previousPos = myTransform.position;
        }

    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void Activate()
    {
        previousPos = myTransform.position;
        checkForCollision = true;
    }

    public void Deactivate()
    {
        checkForCollision = false;
    }

    public void Destroyed()
    {
        gameObject.SetActive(false);
    }




}
