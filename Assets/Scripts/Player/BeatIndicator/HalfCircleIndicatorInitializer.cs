using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class HalfCircleIndicatorInitializer : MonoBehaviour
{
    private void Awake()
    {
        var indicators = GetComponentsInChildren<HalfCircleIndicator>();
        for (var index = 0; index < indicators.Length; index++)
        {
            HalfCircleIndicator i = indicators[index];
            i.start_anim_at_normalized_Time = index* (1.0f / indicators.Length) ;
            i.animationSpeedMultiplier = 1.0f/indicators.Length;
        }
    }
}
