using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelBlock : MonoBehaviour
{

    private Transform myTransform;

    private Rigidbody myRigidBody;

    private LevelBlock myLevelBlockScript;

    [SerializeField]
    private BlockDifficulty myBlockDifficulty;

    [SerializeField]
    private List<ObstacleBlock> myObstacles;

    [SerializeField]
    private List<Collectable> myCollectables;

    [SerializeField]
    private float blockSpeed;

    public Action<BlockDifficulty,LevelBlock> BlockRecycled;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
        myLevelBlockScript = GetComponent<LevelBlock>();
    }

    public void InitializeBlock(Action<CollectableType> _collectableCallback)
    {
        for (int i = 0; i < myObstacles.Count; i++)
        {
            //myObstacles[i].gameObject.SetActive(true);
            myObstacles[i].Initialize();
        }

        for (int i = 0; i < myCollectables.Count; i++)
        {
            myCollectables[i].Initialize(_collectableCallback);
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

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("BlockRecycler"))
        {
            if (BlockRecycled != null)
            {
                for (int i = 0; i < myObstacles.Count; i++)
                {
                    myObstacles[i].Deactivate();
                }
                BlockRecycled(myBlockDifficulty,myLevelBlockScript);
                gameObject.SetActive(false);
            }
        }
        
    }
}
