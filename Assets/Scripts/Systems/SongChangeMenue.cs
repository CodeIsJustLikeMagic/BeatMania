using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongChangeMenue : MonoBehaviour
{
    [SerializeField]
    private Button[] songButtons;

    [SerializeField] private Color runningSongColor;
    [SerializeField] private Color regularSongColor;
    void OnEnable()
    {
        for (int i = 0; i< songButtons.Length; i++)
        {
            songButtons[i].interactable = UnlockedSongs.instance.SongIsUnlocked(i);
            songButtons[i].GetComponent<Text>().color = regularSongColor;
        }
        songButtons[SongchangeSystem.instance.GetCurrentSong()].GetComponent<Text>().color = runningSongColor;
    }
}
