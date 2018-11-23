using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : MonoBehaviour
{
    //working on generating walls
    private static ObstaclePooler instance;
    public static ObstaclePooler Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private ObjectPooler breakableWalls;

    [SerializeField]
    private ObjectPooler barrierWalls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetWall(ObstacleType _type)
    {
        GameObject obj = null;

        switch (_type)
        {
            case ObstacleType.Breakable:
                obj = breakableWalls.GetPooledObject();
                break;

            case ObstacleType.Barrier:
                obj = barrierWalls.GetPooledObject();
                break;

            default:
                Debug.LogError("Invalid Request to Obstalce Pooler");
                break;
        }

        return obj;

    }

}
