﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum CharacterState { Idle, Running, Charging, Dead }
public class Character : MonoBehaviour {

    [SerializeField]
    private CameraFollow cameraFollow = null;

    [SerializeField]
    private ScreenShake screenShake= null;

    [SerializeField]
    private RhinoDetector rhinoDetection = null;

    private Transform myTransform;
    public Transform MyTransform
    {
        get { return myTransform; }
    }
    private Renderer myRenderer;

    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private Image chargeMeter = null;

    [SerializeField]
    private Animator chargeMeterAnimator = null;

    [SerializeField]
    private GameObject shield = null;

    [SerializeField]
    private float laneSize = 0f;

    [SerializeField]
    private float moveSpeed = 0f;

    private bool drainChargePower;
    private bool restoreChargePower;
    private bool unlimitedChargePower;
    private bool chargeButtonHeld;
    private bool shieldActive;

    private float chargePower;

    [SerializeField]
    private float drainSpeed = 0f;

    [SerializeField]
    private float restoreSpeed = 0f;

    [SerializeField]
    private Image chargeButton = null;

    [SerializeField]
    private Sprite chargeOnSprite = null;

    [SerializeField]
    private Sprite chargeOffSprite = null;

    [SerializeField]
    private Sprite noChargeSprite = null;

    [SerializeField]
    private AudioClip hitFenceSound = null;

    [SerializeField]
    private AudioClip hitRockSound = null;

    private CharacterState myCharacterState;

    private Vector3 laneDestination;

    private int laneNumber;

    public Action OnGameOver;

    public Action OnShieldBreak;

    public void Initialize()
    {
        myTransform = GetComponent<Transform>();
        myRenderer = GetComponent<Renderer>();

        chargePower = 100f;
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
                //keyboard controls
                CheckForInput();
                //end keyboard controls
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

    public void GoLeft()
    {
        ChangeLanes(-1);
    }

    public void GoRight()
    {
        ChangeLanes(1);
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
        chargeMeterAnimator.SetTrigger("Collected");

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

        if (chargePower <= Mathf.Epsilon)
        {
            chargeButton.sprite = chargeOffSprite;
        }

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

        restoreChargePower = false;
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
        myAnimator.SetTrigger("Splat");
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
            myAnimator.SetBool("isCharging", true);
            drainChargePower = true;
        }
    }

    public void ChargeEnd()
    {
        if (!unlimitedChargePower)
        {
            if (chargePower <= Mathf.Epsilon)
            {
                chargeButton.sprite = noChargeSprite;
            }
            else
            {
                chargeButton.sprite = chargeOffSprite;
            }
            myCharacterState = CharacterState.Running;
            myAnimator.SetBool("isCharging", false);
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
            if (ParticleManager.Instance != null)
            {
                ParticleManager.Instance.CreateParticles(ParticleType.Hurdle, myTransform.position);
            }
            PlaySound(hitFenceSound);
            if (unlimitedChargePower)
            {
                myAnimator.SetTrigger("Ram");
            }
            _obstacle.Destroyed();
        }
        else
        {
            PlaySound(hitRockSound);
            screenShake.Shake();
            if (shieldActive)
            {
                if (ParticleManager.Instance != null)
                {
                    ParticleManager.Instance.CreateParticles(ParticleType.Hurdle, myTransform.position + Vector3.up);
                }
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

    private void PlaySound(AudioClip _clip)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
        }
    }
}
