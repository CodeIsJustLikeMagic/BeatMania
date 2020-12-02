using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimations : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))//white normal attack 1 (first attack on beat)
        {
            animator.SetTrigger("whiteN1");

        }

        if (Input.GetKeyDown("r"))
        {
            animator.SetTrigger("blueN1");
        }
    }
}
