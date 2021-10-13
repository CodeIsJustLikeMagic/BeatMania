using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlatformBehavior : AlienBehavior
{
    [SerializeField] private GameObject Platform  = null;
    private Animator enemyAnimator3D;
    public void Start()
    {
        if (Platform == null)
        {
            Debug.LogError("Platform Behavior was not assigned a Platform", this);
        }
        enemyAnimator3D = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D;
    }
    public void OnEnable()
    {
        Platform.GetComponent<AlienPlatform>().show();
    }

    public void OnDisable()
    {
        Platform.GetComponent<AlienPlatform>().hide();
    }

    public override void PerformBehaviorOnBeat(float bps)
    {
        Platform.GetComponent<AlienPlatform>().show();
        enemyAnimator3D.SetTrigger("Wait");
    }
}
