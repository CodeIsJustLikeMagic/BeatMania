using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class TutorialTextSystem : MonoBehaviour
{
    public List<TextChange> _tutorialTexts;


    public void OnSchemeChange(PlayerInput playerinput)
    {
        if (playerinput.user.controlScheme.Value.name.Equals("Gamepad"))
        {
            // change tutorial text to gamepad
            foreach (var tutorialText in _tutorialTexts)
            {
                tutorialText.DoTextChange(true);
            }
            //change interaction text
            InteractionHint.Instance.ChangeToGamePad();
            Debug.Log("Controll schema: gamepad");
            
        }
        else
        {
            //change tutorial text to keyboard
            foreach (var tutorialText in _tutorialTexts)
            {
                tutorialText.DoTextChange(false);
            }
            //change interaction text
            InteractionHint.Instance.ChangeToKeyboard();
            Debug.Log("Controll schema: keyboard");
        }
    }
}
