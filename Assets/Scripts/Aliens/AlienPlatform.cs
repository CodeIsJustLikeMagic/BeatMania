using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPlatform : MonoBehaviour
{
    public List<SpriteRenderer> renderers;
    public Collider collider;

    public void Awake()
    {
        hide();
    }

    public void hide()
    {
        collider.enabled = false;
        foreach (var r in renderers)
        {
            r.enabled = false;
        }
    }

    public void show()
    {
        collider.enabled = true;
        foreach (var r in renderers)
        {
            r.enabled = true;
        }
    }
}
