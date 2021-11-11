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
    bool Controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        VersionAKeyboard = VersionAKeyboard.Replace("\\n", "\n");
        VersionBKeyboard = VersionBKeyboard.Replace("\\n", "\n");
        VersionAController = VersionAController.Replace("\\n", "\n");
        VersionBController = VersionBController.Replace("\\n", "\n");

        if (PlayerPrefs.GetInt("Version", 0) == 0)
        {
            if (!Controller)
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
            if (!Controller)
            {
                this.GetComponent<TextMeshPro>().SetText(VersionBKeyboard);
            }
            else
            {
                this.GetComponent<TextMeshPro>().SetText(VersionBController);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
