using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//make "fog" or some obscuring to hide the level blocks poping in.
//still need to make other power ups and connect them
//upgrades system

public class LevelBlock : MonoBehaviour
{

    private Transform myTransform;

    private Rigidbody myRigidBody;

    private LevelBlock myLevelBlockScript;

    [SerializeField]
    private GameObject levelBlockBase;

    [SerializeField]
    private BlockDifficulty myBlockDifficulty;

    private List<ObstacleBlock> myObstacles = new List<ObstacleBlock>();

    [SerializeField]
    private List<Transform> ObstacleLocations;

    [SerializeField]
    private List<ObstacleType> obstacles;

    private List<Collectable> myCollectables = new List<Collectable>();

    [SerializeField]
    private List<Transform> collectableLocations;

    [SerializeField]
    private List<CollectableType> collectables;

    [SerializeField]
    private float blockSpeed;

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

    public void InitializeBlock(Action<CollectableType> _collectableCallback)
    {

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
                newCollectable.Initialize(_collectableCallback, RemoveCollectable);
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

    //private void OnTriggerEnter(Collider _other)
    //{
    //    if (_other.CompareTag("BlockRecycler"))
    //    {

    //        for (int i = 0; i < myObstacles.Count; i++)
    //        {
    //            myObstacles[i].Recycle();
    //        }

    //        for (int i = 0; i < myCollectables.Count; i++)
    //        {
    //            myCollectables[i].Recycle();
    //        }

    //        if (BlockRecycled != null)
    //        {
    //            BlockRecycled(myBlockDifficulty,myLevelBlockScript); 
    //        }
    //        gameObject.SetActive(false);
    //    }
        
    //}
}
