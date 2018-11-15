using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//investigate also generating the barriers as well for easy updating
//need to get rhino snax hooked up for generation
//still need to make other power ups and connect them
//upgrades system
//add in micahs art

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

    public void InitializeBlock(Action<CollectableType> _collectableCallback)
    {
        for (int i = 0; i < myObstacles.Count; i++)
        {
            myObstacles[i].Initialize();
        }

        myCollectables.Clear();
        for (int i = 0; i < collectables.Count; i++)
        {
            if (collectables[i] != CollectableType.None)
            {
                Collectable newCollectable = CollectablePooler.Instance.GetCollectable(collectables[i]).GetComponent<Collectable>();
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

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("BlockRecycler"))
        {

            for (int i = 0; i < myObstacles.Count; i++)
            {
                myObstacles[i].Deactivate();
            }

            for (int i = 0; i < myCollectables.Count; i++)
            {
                myCollectables[i].Recycle();
            }

            if (BlockRecycled != null)
            {
                BlockRecycled(myBlockDifficulty,myLevelBlockScript); 
            }
            gameObject.SetActive(false);
        }
        
    }
}
