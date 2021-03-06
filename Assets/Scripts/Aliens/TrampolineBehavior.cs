﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TrampolineBehavior : AlienBehavior
{
    [SerializeField] private GameObject Trampoline = null;

    public void Start()
    {
        if (Trampoline == null)
        {
            Debug.LogError("Trampolione Behavior was not assigned a Trampoline", this);
        }
    }
    public void OnEnable()
    {
        //Debug.Log("Trampoline Enable");
        Trampoline.SetActive(true);
    }

    public void OnDisable()
    {
        //Debug.Log("Trampoline Disable");
        Trampoline.SetActive(false);
    }

    public override void PerformBehaviorOnBeat(float bps)
    {
        
    }
}
