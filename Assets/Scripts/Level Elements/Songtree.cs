using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songtree : Interactable
{
    [Tooltip("index of song. -1 for 'does not unlock a song'")]
    [SerializeField]
    private int unlocksSong = -1;

    //[SerializeField] private GameObject feedback_light;
    //[SerializeField] private ParticleSystem feedback_particles;

    private void Start()
    {
        if (unlocksSong != -1 || !UnlockedSongs.Instance.SongIsUnlocked(unlocksSong))
        {
            //feedback_light.SetActive(true);
            //feedback_particles.Play();
            interactionText = "learn new song";
        }
        else
        {
            interactionText = "change current song";
        }
    }

    protected override void DoSomething()
    {
        if (unlocksSong == -1 || UnlockedSongs.Instance.SongIsUnlocked(unlocksSong))
        { // not first interaction. Show songchange menu.
            Menu.instance.showSongChangeMenue();
        }
        else
        { // first songtree Interaction. Unlock song and change to it.
            UnlockedSongs.Instance.UnlockSong(unlocksSong);
            SongchangeSystem.Instance.SongChange(unlocksSong);
            interactionText = "change current song";
            //feedback_light.SetActive(true);
            //feedback_particles.Play();
        }
        InteractionHint.Instance.Show(interactionText);
    }

}
