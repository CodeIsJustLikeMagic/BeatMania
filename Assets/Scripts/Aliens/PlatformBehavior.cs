﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlatformBehavior : AlienBehavior
{
    [SerializeField] private GameObject Platform  = null;

    public void Start()
    {
        if (Platform == null)
        {
            Debug.LogError("Platform Behavior was not assigned a Platform", this);
        }
    }
    public void OnEnable()
    {
        Platform.SetActive(true);
    }

    public void OnDisable()
    {
        Platform.SetActive(false);
    }

    public override void PerformBehaviorOnBeat(float bps)
    {
        Platform.SetActive(true);
    }
}
