using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoDetector : MonoBehaviour
{

    private Rigidbody myRigidbody;

    private Transform myTransform;

    private Vector3 previousPos;

    private float blockSpeed;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        previousPos = myTransform.position;
    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myTransform.position + Vector3.back * Time.fixedDeltaTime * blockSpeed);
    }

    public float GetMovementChange()
    {
        Vector3 movementAmount = myTransform.position - previousPos;

        float movementMagnitude = movementAmount.magnitude;

        previousPos = myTransform.position;

        return movementMagnitude;
    }

    public void SetSpeed(float _speed)
    {
        blockSpeed = _speed;
    }

}
