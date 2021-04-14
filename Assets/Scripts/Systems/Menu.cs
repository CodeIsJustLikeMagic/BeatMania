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
    }

    public void showEndOfDemo()
    {
        endofDemo.SetActive(true);
    }

    public void showSongChangeMenue()
    {
        songChangeMenue.SetActive(true);
    }

    public void close()
    {
        endofDemo.SetActive(false);
        songChangeMenue.SetActive(false);
    }
}
