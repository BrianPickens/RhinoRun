using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum CharacterState { Idle, Running, Charging, Dead }
public class Character : MonoBehaviour {

    [SerializeField]
    private CameraFollow cameraFollow = null;

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

    [SerializeField]
    private Image chargeButton;

    [SerializeField]
    private Sprite chargeOnSprite;

    [SerializeField]
    private Sprite chargeOffSprite;

    [SerializeField]
    private Sprite noChargeSprite = null;

    [SerializeField]
    private AudioClip hitFenceSound;

    [SerializeField]
    private AudioClip hitRockSound;

    private Vector3 touchOrigin = Vector3.zero;
    private Vector3 touchCurrent = Vector3.zero;

    private CharacterState myCharacterState;

    private Vector3 laneDestination;

    private int laneNumber;

    public Action OnGameOver;

    public Action OnShieldBreak;

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
        if (myCharacterState != CharacterState.Dead)
        {
            if (myCharacterState == CharacterState.Running || myCharacterState == CharacterState.Charging)
            {
                CheckForInput();
                MoveCharacter();
            }

            if (drainChargePower)
            {
                DrainChargePower();
            }

            CheckForCollision(rhinoDetection.GetMovementChange());

        }
    }

    private void CheckForInput()
    {
        //mobile controls
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 rawTouchOrigin = touch.position;
                touchOrigin = mainCamera.ScreenToWorldPoint(new Vector3(rawTouchOrigin.x, rawTouchOrigin.y, -mainCamera.transform.position.z));
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 rawTouchCurrent = touch.position;
                touchCurrent = mainCamera.ScreenToWorldPoint(new Vector3(rawTouchCurrent.x, rawTouchCurrent.y, -mainCamera.transform.position.z));

                float swipeDistance = Vector3.Distance(touchOrigin, touchCurrent);

                if (swipeDistance > swipeSensitivity && canSwipe)
                {
                    canSwipe = false;
                    if (touchCurrent.x > touchOrigin.x)
                    {
                        ChangeLanes(1);
                    }
                    else
                    {
                        ChangeLanes(-1);
                    }

                    if (myCharacterState == CharacterState.Charging)
                    {
                        ChargeEnd();
                    }
                }

                if (swipeDistance > doubleSwipeSensitivity && canDoubleSwipe && allowDoubleMove)
                {
                    canDoubleSwipe = false;
                    if (touchCurrent.x > touchOrigin.x)
                    {
                        ChangeLanes(1);
                    }
                    else
                    {
                        ChangeLanes(-1);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                canSwipe = true;
                canDoubleSwipe = true;
            }

        }
        //end mobile controls

        //keyboard controls
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLanes(-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLanes(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChargeButton();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ChargeEnd();
        }
        //end keyboard controls
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
        if (!chargeButtonHeld)
        {
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
            chargeButton.sprite = chargeOnSprite;
            myCharacterState = CharacterState.Charging;
            myRenderer.material = ChargeMat;
            drainChargePower = true;
        }
    }

    public void ChargeEnd()
    {
        if (!unlimitedChargePower)
        {
            chargeButton.sprite = chargeOffSprite;
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
            PlaySound(hitRockSound);
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
        }
        else if (_obstacle.MyObstacleType == ObstacleType.Breakable && myCharacterState == CharacterState.Charging)
        {
            screenShake.Shake();
            PlaySound(hitFenceSound);
            _obstacle.Destroyed();
        }
        else
        {
            PlaySound(hitRockSound);
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

    private void PlaySound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
        }
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
