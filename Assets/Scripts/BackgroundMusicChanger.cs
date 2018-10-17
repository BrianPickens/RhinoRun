﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicChanger : MonoBehaviour
{

    [SerializeField]
    private AudioClip backgroundMusicClip;

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ChangeBackgroundMusic(backgroundMusicClip);
        }
    }
}
