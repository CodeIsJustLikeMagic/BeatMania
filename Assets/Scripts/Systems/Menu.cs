using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public GameObject GameMenue;
    public GameObject endofDemo;
    public GameObject songChangeMenue;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public void showEndOfDemo()
    {
        endofDemo.SetActive(true);
    }

    public void showSongChangeMenue()
    {
        songChangeMenue.SetActive(true);
    }

    public void closeAll()
    {
        GameMenue.SetActive(false);
        endofDemo.SetActive(false);
        songChangeMenue.SetActive(false);
    }

    public void closeSongChangeMenue()
    {
        songChangeMenue.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleGameMenu();
        }
    }

    private bool gameMenuOpen = false;
    private void toggleGameMenu()
    {
        if (gameMenuOpen)
        {
            GameMenue.SetActive(false);
            gameMenuOpen = false;
        }
        else
        {
            GameMenue.SetActive(true);
            gameMenuOpen = true;
        }
    }
}
