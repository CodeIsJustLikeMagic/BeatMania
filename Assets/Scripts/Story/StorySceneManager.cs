using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class StorySceneManager : MonoBehaviour
{

    //Sits on the MainCamera in the Story_Intro scene, methods are called by the animations of the camera

    private bool receiveInput = false;
    private Animator anim;

    private InputAction myAction;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();

        myAction = new InputAction(binding: "/*/<button>");
        myAction.performed += context => showNext(context);
        myAction.Enable();
        Invoke("setReceiveInputTrue", 0.2f);
    }

    void showNext(InputAction.CallbackContext value)
    {
        if (receiveInput)
        {
            nextImageTrue();
        }
    }

    public void nextImageTrue()
    {
        anim.SetBool("nextImage", true);
    }

    public void nextImageFalse()
    {
        anim.SetBool("nextImage", false);
    }

    public void setReceiveInputTrue()
    {
        receiveInput = true;
    }
    public void setReceiveInputFalse()
    {
        receiveInput = false;
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene(2);
    }

    private void OnDestroy()
    {
        myAction.Disable();
    }
}
