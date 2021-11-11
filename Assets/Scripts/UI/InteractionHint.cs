using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHint : MonoBehaviour
{
    private static InteractionHint _instance;

    public static InteractionHint Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InteractionHint>();
                if (_instance == null)
                {
                    Debug.LogError("Couldnt find instance of InteractionHint");
                }
            }
            return _instance;
        }
    }
    
    [SerializeField] private Text text = null;
    [SerializeField] private GameObject ui_image = null;

    [SerializeField] private string defaultText="interact";
    [SerializeField] private string keyDescription = "f";

    [SerializeField] private string keyKeyboard = "F";
    [SerializeField] private string keyGamepad = "Y";
    public void Awake()
    {
        Hide();
    }

    public void Show(string interactionHint= "")
    {
        if (interactionHint == "")
        {
            text.text = keyDescription+" "+defaultText;
        }
        else
        {
            text.text = keyDescription+" "+interactionHint;
        }
        ui_image.SetActive(true);
        Canvas.ForceUpdateCanvases();
        ui_image.GetComponent<VerticalLayoutGroup>().enabled = false;
        ui_image.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    public void Hide()
    {
        ui_image.SetActive(false);
    }

    public void ChangeToGamePad()
    {
        keyDescription = keyGamepad;
    }

    public void ChangeToKeyboard()
    {
        keyDescription = keyKeyboard;
    }
}
