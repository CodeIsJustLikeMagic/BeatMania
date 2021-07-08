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
        CursorHide();
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
            CursorHide();
        }
        else
        {
            GameMenue.SetActive(true);
            gameMenuOpen = true;
            CursorShow();
        }
    }

    private void CursorHide()
    {
        #if UNITY_EDITOR
            return;
        #endif
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void CursorShow()
    {
        #if UNITY_EDITOR
            return;
        #endif
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        toggleGameMenu();
    }

    public void Checkpoint()
    {
        GameObject.FindObjectOfType<CharacterController>().ApplyDamage(1000,false,Vector3.zero,0);
        GameMenue.SetActive(false);
        gameMenuOpen = false;
    }
}
