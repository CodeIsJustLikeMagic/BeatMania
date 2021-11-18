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

    private bool receiveInput = true;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void ShowNext(InputAction.CallbackContext value)
    {
        //Debug.Log("ShowNext");
        if (receiveInput && value.started)
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
}
