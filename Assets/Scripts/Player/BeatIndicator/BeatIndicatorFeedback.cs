using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BeatIndicatorFeedback : MonoBehaviour
{
    public static BeatIndicatorFeedback instance;
    
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] float disapearTimer = 0.1f;
    
    private SpriteRenderer renderer;
    private float lastFeedback;

    public void Failed()
    {
        renderer.color = failedColor;
        renderer.enabled = true;
        lastFeedback = Time.time;

    }

    private int failed = 0;
    private int hit = 0;
    private int skipped = 0;
    public void Success()
    {
        renderer.color = successColor;
        renderer.enabled = true;
        lastFeedback = Time.time;
    }
    
    private void Awake()
    {
        instance = this;
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (lastFeedback + disapearTimer <= Time.time)
        {
            renderer.enabled = false;
        }
    }

}
