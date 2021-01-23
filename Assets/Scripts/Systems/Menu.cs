using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public GameObject endofDemo;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public void showEndOfDemo()
    {
        endofDemo.SetActive(true);
    }

    public void close()
    {
        Debug.Log("close");
        endofDemo.SetActive(false);
    }
}
