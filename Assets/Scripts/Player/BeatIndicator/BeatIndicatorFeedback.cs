using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicatorFeedback : MonoBehaviour
{
    public static BeatIndicatorFeedback instance;
    
    public Color successColor;
    public Color failedColor;
    [SerializeField] float disapearTimer = 0.1f;
    
    private SpriteRenderer _spriteRenderer;
    private float _lastFeedback;

    public void Failed()
    {
        _spriteRenderer.color = failedColor;
        _spriteRenderer.enabled = true;
        _lastFeedback = Time.time;

        try
        {
            //Playtest
            PlaytestInstructions.instance.Failed();
        }catch{}
    }

    //private int failed = 0;
    //private int hit = 0;
    //private int skipped = 0;
    public void Success()
    {
        _spriteRenderer.color = successColor;
        _spriteRenderer.enabled = true;
        _lastFeedback = Time.time;

        try
        {
            PlaytestInstructions.instance.Success();
        }catch{}

    }
    
    private void Awake()
    {
        instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_lastFeedback + disapearTimer <= Time.time)
        {
            _spriteRenderer.enabled = false;
        }
    }

}
