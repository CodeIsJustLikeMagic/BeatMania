using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songtree : Interactable
{
    [Tooltip("index of song. -1 for 'does not unlock a song'")]
    [SerializeField]
    private int unlocksSong = -1;

    protected override void DoSomething()
    {
        if (unlocksSong == -1 || UnlockedSongs.instance.SongIsUnlocked(unlocksSong))
        { // not first interaction. Show songchange menu.
            Menu.instance.showSongChangeMenue();
        }
        else
        { // first songtree Interaction. Unlock song and change to it.
            UnlockedSongs.instance.UnlockSong(unlocksSong);
            SongchangeSystem.instance.SongChange(unlocksSong);
            proximityText.SetText("Press f to change current Song");
        }
    }

}
