using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSytem : MonoBehaviour
{
    private static SpaceShipSytem _instance;
    
    public static SpaceShipSytem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SpaceShipSytem>();
            }

            return _instance;
        }
    }
    
    public SpaceShip[] SpaceShips;

    public void LoadCollected(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '1')
            {
                SpaceShips[i].Collect();
            }
        }
    }

    public string SaveCollected()
    {
        string save = "";
        foreach (var ship in SpaceShips)
        {
            if (ship.is_collected)
            {
                save += "1";
            }
            else
            {
                save += "0";
            }
        }
        Debug.Log("Save Space Ship "+save);
        return save;
    }
}
