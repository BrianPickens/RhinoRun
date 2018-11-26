using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : MonoBehaviour
{

    private static ObstaclePooler instance;
    public static ObstaclePooler Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private ObstaclePool breakableWalls;

    [SerializeField]
    private ObstaclePool barrierWalls;

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

    public ObstacleBlock GetObstacle(ObstacleType _type)
    {
        ObstacleBlock obstacle = null;

        switch (_type)
        {
            case ObstacleType.Breakable:
                obstacle = breakableWalls.GetPooledObstacle();
                break;

            case ObstacleType.Barrier:
                obstacle = barrierWalls.GetPooledObstacle();
                break;

            default:
                Debug.LogError("Invalid Request to Obstalce Pooler");
                break;
        }

        return obstacle;

    }

}
