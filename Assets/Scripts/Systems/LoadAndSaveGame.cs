using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAndSaveGame : MonoBehaviour
{
    private static LoadAndSaveGame _instance;

    public static LoadAndSaveGame Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LoadAndSaveGame>();
            }

            return _instance;
        }
    }

    void Start()
    {
        var r = PlayerPrefs.GetInt("LoadSavedState",0);
        if (r==1)
        {
            //Debug.Log("Loading Player State");
            var checkpoint = PlayerPrefs.GetInt("Checkpoint", -1);
            TeleportSystem.Instance.ActivateCheckpoint(checkpoint);
            //Debug.Log("Loaded Checkpoint. Active: "+checkpoint);
            // load position, current song, active checkpoint, space ship parts that are collected
            var x = PlayerPrefs.GetFloat("PlayerX", 0);
            var y = PlayerPrefs.GetFloat("PlayerY", 0);
            CharacterController.Instance.DebugTeleport(new Vector3(x,y+1f,-0.83f));
            //Debug.Log("Loaded Player Position");
            
            var song = PlayerPrefs.GetInt("CurrentSong", 0);
            SongchangeSystem.Instance.Currentsong = song;
            //Debug.Log("Loaded Song. Active: "+song);

            var collectedSpacehShips = PlayerPrefs.GetString("CollectedSpaceShips", "00000000000");
            SpaceShipSytem.Instance.LoadCollected(collectedSpacehShips);
            //Debug.Log("Loaded Collected SpaceShip Parts. Code: "+collectedSpacehShips);

            var unlockedSongs = PlayerPrefs.GetString("UnlockedSongs", "0000");
            UnlockedSongs.Instance.LoadUnlockedSongs(unlockedSongs);
            Debug.Log("Loaded Game State");
        }
        else
        {
            Debug.Log("Creating new Game (by not loading previous gamestate)");
        }
        
        InvokeRepeating("SaveState", 120,30); // autosave every 5 minutes.
        
    }

    public void SaveState()
    {
        PlayerPrefs.SetInt("LoadSavedState",1);
        var v = CharacterController.Instance.transform.position;
        PlayerPrefs.SetFloat("PlayerX", v.x);
        PlayerPrefs.SetFloat("PlayerY", v.y);
        PlayerPrefs.SetInt("Checkpoint", TeleportSystem.Instance.GetActiveCheckpointIndex());
        PlayerPrefs.SetInt("CurrentSong", SongchangeSystem.Instance.Currentsong);
        PlayerPrefs.SetString("CollectedSpaceShips", SpaceShipSytem.Instance.SaveCollected());
        PlayerPrefs.SetString("UnlockedSongs", UnlockedSongs.Instance.SaveUnlockedSongs());
        Debug.Log("SaveState");
        //todo save which songs are unlocked
        MetricWriter.Instance.OnCloseGame();
    }
    
}
