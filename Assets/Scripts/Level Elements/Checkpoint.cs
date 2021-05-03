using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : Interactable
{
    private static Vector3 activeSpawnPoint;
    protected override void DoSomething()
    {
        activeSpawnPoint = this.transform.position;
    }

    public static Vector3 getSpwanPosition()
    {
        return activeSpawnPoint;
    }

    public static void setFirstSpawnPosition(Vector3 position)//first spawn point is where the player loads into the game
    //I'm not creating an explicit checkpoint there because it would be annoying when you decide that you want to move the player spawn location and just move the player object
    //but forget to also move the spawn checkpoint. So the player sets its first position after starting the level to be the first spawn position.
    {
        activeSpawnPoint = position;
    }

}

