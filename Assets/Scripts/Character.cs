using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// add puch button for charge
/// </summary>

public enum CharacterState { Idle, Running, Charging, Dead }
public class Character : MonoBehaviour {


    private Transform myTransform;
    private Renderer myRenderer;

    [SerializeField]
    private Material normalMat;

    [SerializeField]
    private Material ChargeMat;

    [SerializeField]
    private float laneSize;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float chargeDuration;

    [SerializeField]
    private float swipeSensitivity;

    private bool canSwipe;

    private Camera mainCamera;

    private Vector3 touchOrigin = Vector3.zero;
    private Vector3 touchCurrent = Vector3.zero;

    private CharacterState myCharacterState;

    private Vector3 laneDestination;

    private IEnumerator chargeRoutine;

    private int laneNumber;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        myRenderer = GetComponent<Renderer>();
        mainCamera = Camera.main;
    }

    private void Start ()
    {
        canSwipe = true;
        laneNumber = 1;
        laneDestination = myTransform.position;
        myCharacterState = CharacterState.Running;
	}
	
	private void Update ()
    {
        if (myCharacterState == CharacterState.Running || myCharacterState == CharacterState.Charging)
        {
            CheckForInput();
            MoveCharacter();
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
                touchOrigin = Camera.main.ScreenToWorldPoint(new Vector3(rawTouchOrigin.x, rawTouchOrigin.y, -mainCamera.transform.position.z));
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 rawTouchCurrent = touch.position;
                touchCurrent = Camera.main.ScreenToWorldPoint(new Vector3(rawTouchCurrent.x, rawTouchCurrent.y, -mainCamera.transform.position.z));

                float swipeDistance = Vector3.Distance(touchOrigin, touchCurrent);
                Debug.Log(swipeDistance);
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
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                canSwipe = true;
            }
        }
        //end mobile controls

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
            myCharacterState = CharacterState.Charging;
            if (chargeRoutine != null)
            {
                StopCoroutine(chargeRoutine);
            }
            chargeRoutine = ChargeDuration();
            StartCoroutine(chargeRoutine);
        }
    }

    private void MoveCharacter()
    {
        myTransform.position = Vector3.MoveTowards(myTransform.position, laneDestination, Time.deltaTime * moveSpeed);
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
    }

    public void HandleCollision(ObstacleBlock _obstacle)
    {
        if (_obstacle.MyObstacleType == ObstacleType.Barrier)
        {
            Debug.Log("crashed");
        }
        else if (_obstacle.MyObstacleType == ObstacleType.Breakable && myCharacterState == CharacterState.Charging)
        {
            Debug.Log("WEEE");
            _obstacle.Destroyed();
        }
        else
        {
            Debug.Log("crashed fail");
        }
    }

    private IEnumerator ChargeDuration()
    {
        myRenderer.material = ChargeMat;
        yield return new WaitForSeconds(chargeDuration);
        myRenderer.material = normalMat;
        myCharacterState = CharacterState.Running;
    }
}
