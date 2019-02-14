using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum CharacterState { Idle, Running, Charging, Dead }
public class Character : MonoBehaviour {

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private ScreenShake screenShake;

    [SerializeField]
    private RhinoDetector rhinoDetection;

    private Transform myTransform;
    public Transform MyTransform
    {
        get { return myTransform; }
    }
    private Renderer myRenderer;

    [SerializeField]
    private Image chargeMeter;

    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private Material normalMat;

    [SerializeField]
    private Material ChargeMat;

    [SerializeField]
    private Material redMat;

    [SerializeField]
    private float laneSize;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float swipeSensitivity;

    [SerializeField]
    private float doubleSwipeSensitivity;

    [SerializeField]
    private float tapSensitivity;

    private bool canSwipe;
    private bool canDoubleSwipe;
    private float tapDelay;

    private bool drainChargePower;
    private bool restoreChargePower;
    private bool unlimitedChargePower;
    private bool chargeButtonHeld;
    private bool shieldActive;

    private float chargePower;

    [SerializeField]
    private float drainSpeed;

    [SerializeField]
    private float restoreSpeed;

    [SerializeField]
    private Camera mainCamera;

    private Vector3 touchOrigin = Vector3.zero;
    private Vector3 touchCurrent = Vector3.zero;

    private CharacterState myCharacterState;

    private Vector3 laneDestination;

    private int laneNumber;

    public Action OnGameOver;

    public Action OnShieldBreak;

    //test code
    private Vector3 touchPrevious;
    private Vector3 touchNow;
    private Vector3 touchVelocity;
    bool canCalculate;
    private int frameDelay = 3;
    private int currentFrameDelay;
    bool touchingScreen;

    // debug options
    private bool allowDoubleMove = true;

    public void Initialize(float _swipeSensitivity, float _doubleSwipeSensitivity, bool _doubleSwipeOn)
    {
        myTransform = GetComponent<Transform>();
        myRenderer = GetComponent<Renderer>();

        SetSwipeSensitivity(_swipeSensitivity);
        SetDoubleSwipeSensitivity(_doubleSwipeSensitivity);
        SetDoubleSwipeStatus(_doubleSwipeOn);

        chargePower = 100f;
        canSwipe = true;
        laneNumber = 1;
        laneDestination = myTransform.position;
        myCharacterState = CharacterState.Running;

    }

    private void Update()
    {
        //if (myCharacterState != CharacterState.Dead)
        //{
        //    if (myCharacterState == CharacterState.Running || myCharacterState == CharacterState.Charging)
        //    {
                CheckForInput();
            //    MoveCharacter();
            //}

            //if (drainChargePower)
            //{
            //    DrainChargePower();
            //}

            //CheckForCollision(rhinoDetection.GetMovementChange());

        //}
    }

    private void CheckForInput()
    {

        //new hold / swipe

        //check that we are touching the screen
        if (Input.touchCount > 0)
        {
            //get the first touch on the screen
            Touch touch = Input.touches[0];

            //get the first touch's position
            Vector2 rawTouchPosition = touch.position;

           // Debug.Log("Raw touch position: " + rawTouchPosition);
            //convert the touch position to a Vector3 world point
            //Debug.Log("Set now position");
            touchNow = mainCamera.ScreenToWorldPoint(new Vector3(rawTouchPosition.x, rawTouchPosition.y, -mainCamera.transform.position.z));

            //we dont want to calculate on this frame, so we skip over this and wait for canCalculate bool to be turned on by Touchphase.Began
            if (canCalculate)
            {
                //get the velocity vector
                touchVelocity = touchNow - touchPrevious;
                // Debug.Log("now Vector: " + touchNow);
                //  Debug.Log("previous vector: " + touchPrevious);
                //  Debug.Log("Velocity Vector: " + touchVelocity);

                //get the aboslute value of x from the velocity vector
                float xVelocity = touchVelocity.x;
                float absXVelocity = Mathf.Abs(touchVelocity.x);
                //Debug.Log("Velocity: " + absXVelocity);
                if (absXVelocity > 0.001f)
                {
                    Debug.Log("Swipe");
                    if (xVelocity > 0)
                    {
                        ChangeLanes(1);
                    }
                    else if (xVelocity < 0)
                    {
                        ChangeLanes(-1);
                    }
                    {

                    }
                    canCalculate = false;
                }
                else
                {
                    Debug.Log("Hold");
                    ChargeButton();
                    canCalculate = false;
                }
            }

            //allow the touch Velocity Vector to be calculate on the following frame
            if (touch.phase == TouchPhase.Began)
            {
               // Debug.Log("Touch Began phase");
                //canCalculate = true;
                touchingScreen = true;
                currentFrameDelay = frameDelay;

            }
            //reset canCalculate so that the next time we touch the screen is will skip over calculating the velocity vector until the next frame
            else if (touch.phase == TouchPhase.Ended)
            {
               // Debug.Log("Touch Ended Phase");
                canCalculate = false;
                touchingScreen = false;
                ChargeEnd();
            }

            //if we are touching the screen set our current touch as the last touch
            if (currentFrameDelay == frameDelay)
            {
               // Debug.Log("set previous position");
                touchPrevious = touchNow;
            }

            if (touchingScreen)
            {
                currentFrameDelay--;
                if (currentFrameDelay == 0)
                {
                    canCalculate = true;
                }
            }
        }



        //end new controls

        ////mobile controls
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.touches[0];

        //    Vector2 rawTouchPosition = touch.position;

        //    Debug.Log("Set Now");
        //    touchNow = mainCamera.ScreenToWorldPoint(new Vector3(rawTouchPosition.x, rawTouchPosition.y, -mainCamera.transform.position.z));
        //    if (canCalculate)
        //    {
        //        touchVelocity = touchNow - touchPrevious;
        //        float xVelocity = Mathf.Abs(touchVelocity.x);
        //        Debug.Log("now Vector: " + touchNow);
        //        Debug.Log("previous vector: " + touchPrevious);
        //        Debug.Log("Velocity Vector: " + touchVelocity);
        //        Debug.Log("Velocity: " + xVelocity);
        //        if (xVelocity > 0.001f)
        //        {
        //            Debug.Log("Swipe");
        //            canCalculate = false;
        //        }
        //        else
        //        {
        //            Debug.Log("Hold");
        //            canCalculate = false;
        //        }
        //        // Debug.Log(touchNow - touchPrevious);
        //    }
        //    else
        //    {
        //        Debug.Log("one pass");
        //    }
        //    //do calculation with touch previous

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        canCalculate = true;
        //        Vector2 rawTouchOrigin = touch.position;
        //        touchOrigin = mainCamera.ScreenToWorldPoint(new Vector3(rawTouchOrigin.x, rawTouchOrigin.y, -mainCamera.transform.position.z));
        //    }
        //    else if (touch.phase == TouchPhase.Moved)
        //    {
        //        Vector2 rawTouchCurrent = touch.position;
        //        touchCurrent = mainCamera.ScreenToWorldPoint(new Vector3(rawTouchCurrent.x, rawTouchCurrent.y, -mainCamera.transform.position.z));

        //        float swipeDistance = Vector3.Distance(touchOrigin, touchCurrent);

        //        if (swipeDistance > swipeSensitivity && canSwipe)
        //        {
        //            canSwipe = false;
        //            if (touchCurrent.x > touchOrigin.x)
        //            {
        //                ChangeLanes(1);
        //            }
        //            else
        //            {
        //                ChangeLanes(-1);
        //            }

        //            if (myCharacterState == CharacterState.Charging)
        //            {
        //                ChargeEnd();
        //            }
        //        }

        //        if (swipeDistance > doubleSwipeSensitivity && canDoubleSwipe && allowDoubleMove)
        //        {
        //            canDoubleSwipe = false;
        //            if (touchCurrent.x > touchOrigin.x)
        //            {
        //                ChangeLanes(1);
        //            }
        //            else
        //            {
        //                ChangeLanes(-1);
        //            }
        //        }


        //    }
        //    else if (touch.phase == TouchPhase.Ended)
        //    {
        //        canCalculate = false;
        //        canSwipe = true;
        //        canDoubleSwipe = true;
        //        tapDelay = 0f;
        //    }




        //    if (canCalculate)
        //    {
        //        Debug.Log("set Previous");
        //        touchPrevious = touchNow;
        //    }

        //}
        ////end mobile controls

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    ChangeLanes(-1);
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    ChangeLanes(1);
        //}

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    ChargeButton();
        //}

        //if (Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    ChargeEnd();
        //}
    }

    private void MoveCharacter()
    {
        myTransform.position = Vector3.MoveTowards(myTransform.position, laneDestination, Time.deltaTime * moveSpeed);
    }

    private void DrainChargePower()
    {
        if (!restoreChargePower && !unlimitedChargePower)
        {
            chargePower = Mathf.MoveTowards(chargePower, 0, Time.deltaTime * drainSpeed);
            chargeMeter.fillAmount = chargePower / 100;
            if (chargePower <= Mathf.Epsilon)
            {
                ChargeEnd();
            }
        }
    }

    //power up effects
    public void RestoreChargePower(float _amount)
    {
        restoreChargePower = true;
        float restoreLevel = chargePower + _amount;
        if (restoreLevel > 100f)
        {
            restoreLevel = 100f;
        }
        //  Debug.Log("restore started");
        StartCoroutine(RestoreChargePowerRoutine(restoreLevel));
    }

    public void ActivateUnlimitedCharge()
    {
        ChargeStart();
        unlimitedChargePower = true;
    }

    public void DeactivateUnlimitedCharge()
    {
        unlimitedChargePower = false;
        Debug.Log("this happened");
        if (!chargeButtonHeld)
        {
            Debug.Log("we tried to end the charge");
            ChargeEnd();
        }
    }

    public void ActivateShield()
    {
        shieldActive = true;
        shield.SetActive(true);
    }

    public void DeactivateShield()
    {
        shieldActive = false;
        shield.SetActive(false);
    }

    private IEnumerator RestoreChargePowerRoutine(float _restoreLevel)
    {
        while (chargePower < _restoreLevel - Mathf.Epsilon)
        {
            chargePower = Mathf.MoveTowards(chargePower, _restoreLevel, Time.deltaTime * restoreSpeed);
            chargeMeter.fillAmount = chargePower / 100;
            if (_restoreLevel - chargePower <= Mathf.Epsilon)
            {
                restoreChargePower = false;
                break;
            }
            yield return null;
        }
      //  Debug.Log("restore ended");
        
    }

    private void ChangeLanes(int _direction)
    {
        laneNumber += _direction;
        if (laneNumber < 0)
        {
            laneNumber = 0;
        }

        if (laneNumber > 2)
        {
            laneNumber = 2;
        }

        switch (laneNumber)
        {
            case 0:
                laneDestination = new Vector3(-laneSize, myTransform.position.y, 0);
                break;

            case 1:
                laneDestination = new Vector3(0, myTransform.position.y, 0);
                break;

            case 2:
                laneDestination = new Vector3(laneSize, myTransform.position.y, 0);
                break;

            default:
                Debug.LogError("Changed lanes into strange lane");
                break;
        }

        cameraFollow.SetTargetPosition(laneNumber);
    }

    private void GameOver()
    {
        myCharacterState = CharacterState.Dead;
        myRenderer.material = redMat;
        if (OnGameOver != null)
        {
            OnGameOver();
        }

    }

    public void ChargeButton()
    {
        chargeButtonHeld = true;
        ChargeStart();
    }

    public void ChargeStart()
    {
        if (chargePower > Mathf.Epsilon)
        {
            myCharacterState = CharacterState.Charging;
            StartCoroutine(ChargeDelay());
        }
    }

    //delay the charge drain until we know for sure this is a tap and hold
    private IEnumerator ChargeDelay()
    {
        yield return new WaitForSeconds(0.015f);
        if (myCharacterState == CharacterState.Charging)
        {
            myRenderer.material = ChargeMat;
            drainChargePower = true;
            
        }

    }

    public void ChargeEnd()
    {
        if (!unlimitedChargePower)
        {
            myCharacterState = CharacterState.Running;
            myRenderer.material = normalMat;
            drainChargePower = false;
        }
        chargeButtonHeld = false;
    }

    private void CheckForCollision(float _movementAmount)
    {
        RaycastHit hitInfo;
        Vector3 offset = new Vector3(0f, 0f, 0.5f);
        if (Physics.Raycast(myTransform.position + offset, Vector3.back, out hitInfo, _movementAmount, 1 << 0))
        {
            if (hitInfo.collider.CompareTag("ChargeObstacle"))
            {
               HandleCollision(hitInfo.collider.GetComponent<ObstacleBlock>());
            }
        }
    }

    public void HandleCollision(ObstacleBlock _obstacle)
    {
        if (_obstacle.MyObstacleType == ObstacleType.Barrier)
        {
            screenShake.Shake();
            if (shieldActive)
            {
                _obstacle.Destroyed();
                if (OnShieldBreak != null)
                {
                    OnShieldBreak();
                }
            }
            else
            {
                GameOver();
            }
           // Debug.Log("crashed");
        }
        else if (_obstacle.MyObstacleType == ObstacleType.Breakable && myCharacterState == CharacterState.Charging)
        {
            //Debug.Log("broke wall");
            screenShake.Shake();
            _obstacle.Destroyed();
        }
        else
        {
            screenShake.Shake();
            if (shieldActive)
            {
                _obstacle.Destroyed();
                if (OnShieldBreak != null)
                {
                    OnShieldBreak();
                }
            }
            else
            {
                GameOver();
            }
            //Debug.Log("crashed fail");
        }
    }

    public void SetSwipeSensitivity(float _sensitivity)
    {
        swipeSensitivity = _sensitivity;
    }

    public void SetDoubleSwipeSensitivity(float _sensitivity)
    {
        doubleSwipeSensitivity = _sensitivity;
    }

    public void SetDoubleSwipeStatus(bool _status)
    {
        allowDoubleMove = _status;
    }


    //debug options
    public void DoubleMove()
    {
        if (allowDoubleMove)
        {
            allowDoubleMove = false;
        }
        else
        {
            allowDoubleMove = true;
        }
    }
}
