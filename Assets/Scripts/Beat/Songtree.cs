using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songtree : MonoBehaviour
{
    [Tooltip("index of song. -1 for 'does not unlock a song'")]
    [SerializeField]
    private int unlocksSong = -1;
    bool playerinside = false;

    private void Update()
    {
        if (playerinside)
        {
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.F))
            {
                if (unlocksSong == -1 || UnlockedSongs.instance.SongIsUnlocked(unlocksSong))
                { // not first interaction. Show songchange menu.
                    Menu.instance.showSongChangeMenue();
                }
                else
                { // first songtree Interaction. Unlock song and change to it.
                    UnlockedSongs.instance.UnlockSong(unlocksSong);
                    SongchangeSystem.instance.SongChange(unlocksSong);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinside = false;
        }
    }
}
