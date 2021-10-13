using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSavedState : MonoBehaviour
{
    private static LoadSavedState _instance;

    public static LoadSavedState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LoadSavedState>();
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
        }
        else
        {
            Debug.Log("Creating new Game (by not loading previous gamestate)");
        }
        
        InvokeRepeating("SaveState", 300,300); // autosave every 5 minutes.
        
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
        Debug.Log("SaveState");
        //todo save which songs are unlocked
    }
    
}
