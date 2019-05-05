using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//release reminders
//need to adjust value of upgrades in save manager so that the valuation of local vs cloud is correct

//For Tomorrow:
//Title Scene Art
//icon art
//revist loading screen

//BUGS
//can unpause tutorial by presssing settings

//REQUIRED FOR RELEASE
//rhino
//character animations
//music - get music from blake

//POST COMPLETION SYSTEMS
//optimization - batching - the coins have a bathcing issue - turn off shadows on things if they dont need it

//BALANCING AFTER PRODUCT IS READY
//how often power ups drop
//how fast things speed up
//speed cap
//how fast do we increase difficulty
//what are the different levels of difficulty
//adjust power up and staminia spawn rates

//GAMEPLAY HELP
//make more variation of level blocks that more barriers

//IF I EVER REVIST THIS
//daily reward coins
//googleplay
//analytics


public class LevelBlock : MonoBehaviour
{

    private Transform myTransform;

    private Rigidbody myRigidBody;

    private LevelBlock myLevelBlockScript;

    [SerializeField]
    private GameObject levelBlockBase = null;

    [SerializeField]
    private BlockDifficulty myBlockDifficulty = BlockDifficulty.None;

    private List<ObstacleBlock> myObstacles = new List<ObstacleBlock>();

    [SerializeField]
    private List<Transform> ObstacleLocations = new List<Transform>();

    [SerializeField]
    private List<ObstacleType> obstacles = new List<ObstacleType>();

    private List<Collectable> myCollectables = new List<Collectable>();

    [SerializeField]
    private List<Transform> collectableLocations = new List<Transform>();

    [SerializeField]
    private List<CollectableType> collectables = new List<CollectableType>();

    [SerializeField]
    private UnityEngine.UI.Text distanceText = null;

    [SerializeField]
    private float blockSpeed = 0f;

    public Action<BlockDifficulty,LevelBlock> BlockRecycled;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
        myLevelBlockScript = GetComponent<LevelBlock>();
    }

    private void Start()
    {
        GameObject myBase = (GameObject)Instantiate(levelBlockBase);
        myBase.transform.SetParent(myTransform);
        myBase.transform.localPosition = Vector3.zero;
    }

    public void InitializeBlock(Action<CollectableType> _collectableCallback, int _distanceMarker, float _coinRotationOffset)
    {

        //initialize distanceMarker
        distanceText.text = "" + _distanceMarker;

        //initialize obstacles
        myObstacles.Clear();
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i] != ObstacleType.None)
            {
                ObstacleBlock newObstacle = ObstaclePooler.Instance.GetObstacle(obstacles[i]);
                newObstacle.SetLocation(ObstacleLocations[i]);
                newObstacle.Initialize();
                myObstacles.Add(newObstacle);

            }
        }

        //initialize collectables
        myCollectables.Clear();
        for (int i = 0; i < collectables.Count; i++)
        {
            if (collectables[i] != CollectableType.None)
            {
                Collectable newCollectable = CollectablePooler.Instance.GetCollectable(collectables[i]);
                newCollectable.SetLocation(collectableLocations[i]);
                newCollectable.Initialize(_collectableCallback, RemoveCollectable, _coinRotationOffset);
                myCollectables.Add(newCollectable);
            }
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.MovePosition(myTransform.position + Vector3.back * Time.fixedDeltaTime * blockSpeed);
    }

    public Vector3 GetPosition()
    {
        return myTransform.position;
    }

    public void SetPosition(Vector3 _newPosition)
    {
        myTransform.position = _newPosition;
        for (int i = 0; i < myObstacles.Count; i++)
        {
            myObstacles[i].Activate();
        }
    }

    public void SetSpeed(float _speed)
    {
        blockSpeed = _speed;
    }

    private void RemoveCollectable(Collectable _collectable)
    {
        if (myCollectables.Contains(_collectable))
        {
            myCollectables.Remove(_collectable);
        }
    }

    public void RecycleBlock()
    {
        for (int i = 0; i < myObstacles.Count; i++)
        {
            myObstacles[i].Recycle();
        }

        for (int i = 0; i < myCollectables.Count; i++)
        {
            myCollectables[i].Recycle();
        }

        if (BlockRecycled != null)
        {
            BlockRecycled(myBlockDifficulty, myLevelBlockScript);
        }
        gameObject.SetActive(false);
    }

}
