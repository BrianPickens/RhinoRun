using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour {
    
    [SerializeField]
    private ObstacleBlock pooledObstacle = null;

    List<ObstacleBlock> pooledObstacles = new List<ObstacleBlock>();

    [SerializeField]
    private int startAmount = 0;

    private void Start()
    {
        for (int i = 0; i < startAmount; i++)
        {
            ObstacleBlock newObstacle = (ObstacleBlock)Instantiate(pooledObstacle);
            pooledObstacles.Add(newObstacle);
            newObstacle.transform.SetParent(gameObject.transform);
        }
    }

    public ObstacleBlock GetPooledObstacle()
    {
        for (int i = 0; i < pooledObstacles.Count; i++)
        {
            if (!pooledObstacles[i].gameObject.activeInHierarchy)
            {
                return pooledObstacles[i];
            }
        }

        ObstacleBlock newObstacle = (ObstacleBlock)Instantiate(pooledObstacle);
        pooledObstacles.Add(newObstacle);
        newObstacle.transform.SetParent(gameObject.transform);
        return newObstacle;
    }

}
