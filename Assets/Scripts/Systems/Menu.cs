using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public GameObject GameMenue;
    public GameObject endofDemo;
    public GameObject songChangeMenue;

    public UnityEvent songChangeMenuEvent;
    public UnityEvent mainMenuEvent;
    

    private PlayerInput _playerInput;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        CursorHide();
        _playerInput = FindObjectOfType<PlayerInput>();
        if (songChangeMenuEvent == null)
        {
            songChangeMenuEvent = new UnityEvent();
        }

        if (mainMenuEvent == null)
        {
            mainMenuEvent = new UnityEvent();
        }
        ResumeGame();
    }

    public void showEndOfDemo()
    {
        endofDemo.SetActive(true);
    }

    public void showSongChangeMenue()
    {
        //PauseGame();
        _playerInput.SwitchCurrentActionMap("menu");
        songChangeMenue.SetActive(true);
        CursorShow();
        songChangeMenuEvent.Invoke();
        // select first song
    }

    public void PauseGame()
    {
        _playerInput.SwitchCurrentActionMap("menu");
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _playerInput.SwitchCurrentActionMap("gameplay");
        Time.timeScale = 1;
    }

    public void closeAll()
    {
        GameMenue.SetActive(false);
        endofDemo.SetActive(false);
        songChangeMenue.SetActive(false);
        CursorHide();
        ResumeGame();
    }

    public void closeSongChangeMenue()
    {
        songChangeMenue.SetActive(false);
        CursorHide();
        ResumeGame();
    }

    public void OnMenuOpen(InputAction.CallbackContext value)
    {
        if (value.started)
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
            ResumeGame();
        }
        else
        {
            GameMenue.SetActive(true);
            mainMenuEvent.Invoke();
            gameMenuOpen = true;
            CursorShow();
            PauseGame();
        }
    }

    public static void CursorHide()
    {
        #if UNITY_EDITOR
            return;
        #endif
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public static void CursorShow()
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
        GameObject.FindObjectOfType<CharacterController>().ApplyDamage(1000,false,Vector3.zero,"resetToCheckpoint",0);
        GameMenue.SetActive(false);
        gameMenuOpen = false;
        CursorHide();
        ResumeGame();
    }
}
