﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSound : MonoBehaviour
{
    AudioManager audiomanager;

    float volume;

    // Start is called before the first frame update
    void Start()
    {

        audiomanager = AudioManager.Instance;

        volume = audiomanager.Volume;

        GetComponent<AudioSource>().volume = volume;

    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.isFaded)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
