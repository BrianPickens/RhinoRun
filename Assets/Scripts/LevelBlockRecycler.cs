using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelBlockRecycler : MonoBehaviour {

    public Action RecycleBlock;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("LevelBlock"))
        {

            _other.GetComponent<LevelBlock>().RecycleBlock();
            if (RecycleBlock != null)
            {
                RecycleBlock();
            }
        }
    }

}
