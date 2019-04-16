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

    void Start()
    {
        int randomIndex = Random.Range(0, boulderEdges.Count);
        GameObject boulderEdge = Instantiate(boulderEdges[randomIndex]);
        boulderEdge.transform.parent = this.gameObject.transform;
        boulderEdge.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    private void RandomizeTrees()
    {
        List<Transform> treeTransforms = new List<Transform>();
        for (int i = 0; i < leftInnerTrees.Count; i++)
        {
            treeTransforms.Add(leftInnerTrees[i].transform);
        }
    }
}
