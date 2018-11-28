using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform leftEdge;

    [SerializeField]
    private Transform rightEdge;

    [SerializeField]
    private Transform middle;

    [SerializeField]
    private float moveSpeed;

    private Vector3 targetVector;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        SetTargetPosition(1);
    }

    private void LateUpdate()
    {
        myTransform.position = Vector3.MoveTowards(myTransform.position, targetVector, Time.deltaTime * moveSpeed);
    }

    public void SetTargetPosition(int _laneNumber)
    {
        switch (_laneNumber)
        {
            case 0:
                targetVector = leftEdge.position;
                break;

            case 1:
                targetVector = middle.position;
                break;

            case 2:
                targetVector = rightEdge.position;
                break;

            default:
                Debug.LogError("invalid lane sent to cameraFollow");
                break;
        }
    }



}
