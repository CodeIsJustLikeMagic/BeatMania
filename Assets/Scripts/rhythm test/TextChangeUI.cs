using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TextChangeUI : MonoBehaviour
{
    public string VersionAKeyboard;
    public string VersionAController;

    private void Awake()
    {
        DoTextChange(true);
    }

    public void OnSchemeChange(PlayerInput playerinput)
    {
        if (playerinput.user.controlScheme.Value.name.Equals("Gamepad"))
        {
            DoTextChange(true);
            
        }
        else
        {
            DoTextChange(false);
        }
    }
    public void DoTextChange(bool controller)
    {
        //if (Input.GetJoystickNames().Length > 0) Controller = true;
        VersionAKeyboard = VersionAKeyboard.Replace("\\n", "\n");
        VersionAController = VersionAController.Replace("\\n", "\n");
        if (!controller)
        {
                
            this.GetComponent<Text>().text = VersionAKeyboard;
        }
        else
        {
            this.GetComponent<Text>().text = VersionAController;
        }
    }
}
