using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    bool dashL = false;
    bool dashR = false;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        //LT und RT --> Input Manager 
        if (Input.GetAxis("LT") > 0 || Input.GetKeyDown(KeyCode.Q))
        {
            dashL = true;
        }
        if (Input.GetAxis("RT") > 0 || Input.GetKeyDown(KeyCode.E))
        {
            dashR = true;
        }
    }

    public void OnFall()
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dashL, dashR);
        jump = false;
        dashL = false;
        dashR = false;
    }
}
