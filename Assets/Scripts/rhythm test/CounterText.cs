using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CounterText : MonoBehaviour
{
    public int current = 0;
    public int maxNumber = 2;
    public string Text = "Phase: ";

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void OnIncrease()
    {
        current++;
        _text.text = Text + current +"/" +maxNumber;
    }

    public void OnRest()
    {
        current = 0;
        _text.text = Text + current +"/" +maxNumber;
    }
    
}
