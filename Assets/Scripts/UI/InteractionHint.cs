using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHint : MonoBehaviour
{
    public static InteractionHint instance;
    [SerializeField] private Text text = null;
    [SerializeField] private GameObject ui_image = null;

    [SerializeField] private string defaultText="interact";
    [SerializeField] private string keyDescription = "f";
    public void Awake()
    {
        instance = this;
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
    
}
