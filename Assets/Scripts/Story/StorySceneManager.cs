using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Update is called once per frame
    void Update()
    {
        if (receiveInput)
        {
            if (Input.anyKey)
            {
                nextImageTrue();
            }
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
