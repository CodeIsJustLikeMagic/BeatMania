using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibingEntity : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        AnimationOnBeat.instance.AddMyselfToList(this);
    }

    public void OnBeat(float speed)
    {
        anim.SetTrigger("beat");
        Debug.Log(gameObject.name+ " setAnimLength ");
        anim.SetFloat("animSpeed", speed);
        //Debug.Log("anim speed is " + anim.speed);
        //restart animationNow
        
    }
}
