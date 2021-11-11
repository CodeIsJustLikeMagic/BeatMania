using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;

    public float runSpeed = 40f;

    //float horizontalMove = 0f;
    bool jump = false;

    bool dashL = false;
    bool dashR = false;

    public float moveVal;

    public void OnMovement(InputAction.CallbackContext value)
    {
        moveVal = value.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            jump = true;
        }
        
    }

    public void OnDashright(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            dashR = true;
        }
        
    }

    public void OnDashleft(InputAction.CallbackContext value)
    {
        if (value.started)
        {
           dashL = true; 
        }
    }
    

    // Update is called once per frame
    void Update()
    {

        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(moveVal * runSpeed));
        
        controller.Move(moveVal * runSpeed * Time.fixedDeltaTime, jump, dashL, dashR);
        jump = false;
        dashL = false;
        dashR = false;
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
        //controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dashL, dashR);
        //jump = false;
        //dashL = false;
        //dashR = false;
    }

    public float getHorizontalMove()
    {
        return moveVal * runSpeed;
    }
}
