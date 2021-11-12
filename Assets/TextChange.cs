using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChange : MonoBehaviour
{
    public string VersionAKeyboard;
    public string VersionBKeyboard;
    public string VersionAController;
    public string VersionBController;

    private void Awake()
    {
        DoTextChange(true);
    }

    public void DoTextChange(bool controller)
    {
        //if (Input.GetJoystickNames().Length > 0) Controller = true;
        VersionAKeyboard = VersionAKeyboard.Replace("\\n", "\n");
        VersionBKeyboard = VersionBKeyboard.Replace("\\n", "\n");
        VersionAController = VersionAController.Replace("\\n", "\n");
        VersionBController = VersionBController.Replace("\\n", "\n");

        if (PlayerPrefs.GetInt("Version", 0) == 0)
        {
            if (!controller)
            {
                
                this.GetComponent<TextMeshPro>().SetText(VersionAKeyboard);
            }
            else
            {
                this.GetComponent<TextMeshPro>().SetText(VersionAController);
            }
            
        }
        else
        {
            if (!controller)
            {
                this.GetComponent<TextMeshPro>().SetText(VersionBKeyboard);
            }
            else
            {
                this.GetComponent<TextMeshPro>().SetText(VersionBController);
            }
        }
    }
}
