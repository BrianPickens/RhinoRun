using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlockBase : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> boulderEdges = new List<GameObject>();

    [SerializeField]
    private List<GameObject> innerTreeLocations = new List<GameObject>();

    [SerializeField]
    private List<GameObject> outterTreeLocations = new List<GameObject>();

    void Start()
    {
        int randomIndex = Random.Range(0, boulderEdges.Count);
        GameObject boulderEdge = Instantiate(boulderEdges[randomIndex]);
        boulderEdge.transform.parent = this.gameObject.transform;
        boulderEdge.transform.localPosition = new Vector3(0f, 0f, 0f);
    }


}
