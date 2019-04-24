using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform leftEdge = null;

    [SerializeField]
    private Transform rightEdge = null;

    [SerializeField]
    private Transform middle = null;

    [SerializeField]
    private float moveSpeed = 0f;

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
