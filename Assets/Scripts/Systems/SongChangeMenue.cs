using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongChangeMenue : MonoBehaviour
{
    [SerializeField]
    private Button[] songButtons;
    void OnEnable()
    {
        for (int i = 0; i< songButtons.Length; i++)
        {
            songButtons[i].interactable = true; //UnlockedSongs.instance.SongIsUnlocked(i);
        }
    }
}
