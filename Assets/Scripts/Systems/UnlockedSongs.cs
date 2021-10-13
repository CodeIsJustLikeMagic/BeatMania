using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedSongs : MonoBehaviour
{
    private static UnlockedSongs _instance;
    public static UnlockedSongs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UnlockedSongs>();
            }

            return _instance;
        }
    }
    
    [SerializeField]
    private bool[] song_unlocked = null;

    public void UnlockSong(int song)
    {
        if(song > 0 && song < song_unlocked.Length)
        {
            song_unlocked[song] = true;
        }

    }

    /// <summary>
    /// returns true if the player has unlocked the given song
    /// </summary>
    /// <param name="song"></param>
    /// <returns></returns>
    public bool SongIsUnlocked(int song)
    {
        #if UNITY_EDITOR
            return true;
        #endif
        if(song < 0)
        {
            return false;
        }
        if(song > song_unlocked.Length)
        {
            Debug.LogError("There is no Entry for song "+song+" in UnlockedSongs");
            return false;
        }
        return song_unlocked[song];
    }
}
