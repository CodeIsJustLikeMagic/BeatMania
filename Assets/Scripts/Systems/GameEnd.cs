using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private static GameEnd _instance;
    
    public static GameEnd Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameEnd>();
            }

            return _instance;
        }
    }

    public void EndReached()
    {
        Debug.Log("Game is Finished");
        SceneManager.LoadScene(0); // maybe create a new scene just for this ? a version of the story into
    }
}
