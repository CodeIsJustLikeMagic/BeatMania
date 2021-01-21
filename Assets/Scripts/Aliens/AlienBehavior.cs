using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class AlienBehavior : MonoBehaviour
{
    public abstract void PerformBehaviorOnBeat(float bps);
}


