﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    [SerializeField]
    private float duration = 0;

    private void OnEnable()
    {
        Invoke("HideParticle", duration);
    }

    private void HideParticle()
    {
        gameObject.SetActive(false);
    }

}
