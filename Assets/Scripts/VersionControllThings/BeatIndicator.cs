using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{
    private static BeatIndicator _instance;
    
    public static BeatIndicator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BeatIndicator>();
                if (_instance == null)
                {
                    Debug.LogError("Couldnt find instance of CharacterController");
                }
            }

            return _instance;
        }
    }
    
    public List<Renderer> _renderers;
    public BeatCheckerIndicator beatCheckerIndicator;
    public BeatIndicatorFeedback beatIndicatorFeedback;
    public void Hide()
    {
        foreach (var ren in _renderers)
        {
            ren.enabled = false;
        }

        beatCheckerIndicator.enabled = false;
        beatIndicatorFeedback.enabled = false;
    }

    public void Show()
    {
        foreach (var ren in _renderers)
        {
            ren.enabled = true;
        }

        beatCheckerIndicator.enabled = true;
        beatIndicatorFeedback.enabled = true;
    }
}
