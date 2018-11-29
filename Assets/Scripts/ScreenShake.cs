using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    [SerializeField]
    private Transform shakeAxis;

    private int shakeCount;

    private float shakeIntensity;
    private float shakeSpeed;

    private Vector3 nextShakePosition;

    void Update()
    {

        if (Vector3.Distance(shakeAxis.transform.position, Vector3.zero) > Mathf.Epsilon)
        {
            shakeAxis.localPosition = Vector3.MoveTowards(shakeAxis.localPosition, nextShakePosition, Time.deltaTime * shakeSpeed);
        }


        if (Vector2.Distance(shakeAxis.localPosition, nextShakePosition) < shakeIntensity / 5f)
        {
            shakeCount--;
            shakeIntensity -= 0.1f;

            if (shakeCount <= 1)
            {
                nextShakePosition = new Vector3(0, 0, shakeAxis.localPosition.z);
            }
            else
            {
                DetermineNextShakePosition();
            }
        }
    }

    public void Shake(float _intensity = 0.5f, int _numShakes = 4, float _speed = 10f)
    {
        shakeCount = _numShakes;
        shakeIntensity = _intensity;
        shakeSpeed = _speed;

        DetermineNextShakePosition();
    }


    private void DetermineNextShakePosition()
    {
        nextShakePosition = new Vector3(Random.Range(-shakeIntensity, shakeIntensity),
            Random.Range(-shakeIntensity, shakeIntensity),
            shakeAxis.localPosition.z);
    }
}
