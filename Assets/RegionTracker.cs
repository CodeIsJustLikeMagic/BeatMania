using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionTracker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MetricWriter.Instance.WriteVariousMetric(gameObject.name);
        }
    }
}
