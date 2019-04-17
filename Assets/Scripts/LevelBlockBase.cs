using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlockBase : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> boulderEdges = new List<GameObject>();

    [SerializeField]
    private List<GameObject> leftInnerTrees = new List<GameObject>();

    [SerializeField]
    private List<GameObject> leftOutterTrees = new List<GameObject>();

    [SerializeField]
    private List<GameObject> rightInnerTrees = new List<GameObject>();

    [SerializeField]
    private List<GameObject> rightOuterTrees = new List<GameObject>();

    [SerializeField]
    private List<Transform> leftInnerLocations = new List<Transform>();

    [SerializeField]
    private List<Transform> leftOutterLocations = new List<Transform>();

    [SerializeField]
    private List<Transform> rightInnerLocations = new List<Transform>();

    [SerializeField]
    private List<Transform> rightOurterLocations = new List<Transform>();

    void Start()
    {
        int randomIndex = Random.Range(0, boulderEdges.Count);
        GameObject boulderEdge = Instantiate(boulderEdges[randomIndex]);
        boulderEdge.transform.parent = this.gameObject.transform;
        boulderEdge.transform.localPosition = new Vector3(0f, 0f, 0f);

        RandomizeTrees();
    }

    private void RandomizeTrees()
    {
        for (int i = 0; i < leftInnerLocations.Count; i++)
        {
            int randomIndex = Random.Range(0, leftInnerTrees.Count);
            leftInnerTrees[randomIndex].transform.position = leftInnerLocations[i].position;
            leftInnerTrees.Remove(leftInnerTrees[randomIndex]);
        }

        for (int i = 0; i < leftOutterLocations.Count; i++)
        {
            int randomIndex = Random.Range(0, leftOutterTrees.Count);
            leftOutterTrees[randomIndex].transform.position = leftOutterLocations[i].position;
            leftOutterTrees.Remove(leftOutterTrees[randomIndex]);
        }

        for (int i = 0; i < rightInnerLocations.Count; i++)
        {
            int randomIndex = Random.Range(0, rightInnerTrees.Count);
            rightInnerTrees[randomIndex].transform.position = rightInnerLocations[i].position;
            rightInnerTrees.Remove(rightInnerTrees[randomIndex]);
        }

        for (int i = 0; i < rightOurterLocations.Count; i++)
        {
            int randomIndex = Random.Range(0, rightOuterTrees.Count);
            rightOuterTrees[randomIndex].transform.position = rightOurterLocations[i].position;
            rightOuterTrees.Remove(rightOuterTrees[randomIndex]);
        }


    }
}
