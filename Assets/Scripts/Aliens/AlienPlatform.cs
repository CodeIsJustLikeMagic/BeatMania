using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AlienPlatform : MonoBehaviour
{
    public List<SpriteRenderer> renderers;
    [FormerlySerializedAs("collider")] public Collider my_collider;

    public void Awake()
    {
        hide();
    }

    public void hide()
    {
        my_collider.enabled = false;
        foreach (var r in renderers)
        {
            r.enabled = false;
        }
    }

    public void show()
    {
        my_collider.enabled = true;
        foreach (var r in renderers)
        {
            r.enabled = true;
        }
    }
}
