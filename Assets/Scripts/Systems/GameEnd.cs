using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public static GameEnd instance;

    private void Awake()
    {
        instance = this;
    }

    public void EndReached()
    {
        Debug.Log("Game is Finished");
        SceneManager.LoadScene(0); // maybe create a new scene just for this ? a version of the story into
    }
}
