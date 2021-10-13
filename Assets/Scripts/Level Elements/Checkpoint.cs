using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : Interactable
{
    private static Vector3 activeSpawnPoint;
    [SerializeField] private GameObject feedbackLight = null;
    [SerializeField] private ParticleSystem feedbackParticleSystem = null;

    private void Start()
    {
        FeedbackInactive();
    }

    public void Activate()
    {
        DoSomething();
    }

    protected override void DoSomething()
    {
        Debug.Log("Checkpoint used");
        var checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (var s in checkpoints)
        {
            s.FeedbackInactive(); // Deactivate Feedback for previous Checkpoint
        }

        activeSpawnPoint = getPosition();
        FeedbackActive(); // Activate Feedback for Current Checkpoint
    }

    public Vector3 getPosition()
    {
        Vector3 p = this.transform.position;
        p.z = -0.83f;
        p.y += 1f;
        return p;
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

    public bool active = false;
    private void FeedbackActive()
    {
        feedbackLight.SetActive(true);
        feedbackParticleSystem.Play();
        active = true;
    }
    private void FeedbackInactive()
    {
        feedbackLight.SetActive(false);
        feedbackParticleSystem.Stop();
        active = false;
    }

}

