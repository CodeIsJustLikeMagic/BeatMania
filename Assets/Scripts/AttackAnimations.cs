﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class just does almost everything right now
public class AttackAnimations : MonoBehaviour
{
    public Animator playerAnimator; //animator is set though inspector in unity editor
    // Start is called before the first frame update

    //Input
    void TestInput()
    {
        if (Input.GetKeyDown("e"))//white normal attack 1 (first attack on beat)
        {

            Attack();
        }
    }
    // Update is called once per frame
    void Update()
    {
        TestInput();
    }
    //Hold Beat
    float beatStart = 0;
    float beatLength = 0;
    float toleranceRange = 0.07f;//
    public void ListenToSongChange()
    {
       beatStart = AnimationOnBeat.instance.beatStart;
       beatLength = AnimationOnBeat.instance.beatLength;
    }

    bool InBeat()//is current time in beat?
    {
        float now = Time.time;
        //now is in beat if: now == beatStart + n * beatLegnth +- toleranceRange; where n is any natural number
        float missedBySeconds = (now - beatStart) % beatLength; //missed the beat by seconds.miliseconds. 
        //allways oriented toward the next comming beat. 
        //so if you hit the beat within the tolerance range its either almost beatLength, or almost 0 
        //Debug.Log("InBeat " + missedBySeconds+ " ranges: 0-"+toleranceRange+"; "+(beatLength-toleranceRange)+"-"+beatLength);
        return missedBySeconds <= toleranceRange || missedBySeconds >= beatLength-toleranceRange;     
    }

    // Combosystem
    private int combocounter = 0;
    int maxCombo = 5;

    void Attack()
    {
        Debug.Log("combocounter: " + combocounter);
        if (InBeat())
        {
            playerAnimator.SetTrigger("TAN"+(combocounter+1)); // Test Attack Normal 1-5
            combocounter = (combocounter + 1) % maxCombo;
        }
        else
        {
            combocounter = 0; //reset combo
            playerAnimator.SetTrigger("whiteN1"); // do weak attack
        }
    }

    //notizen des combosystems:
    //maximal 5 angriffe
    //jeder weitere angriff erhöht Schaden auf 3x
    //dmg, 2dmg, 3dmg, 3dmg, 3dmg
    //Angriffe 3,4,5 staggern
    //Schwacher Angriff: 1/2 dmg

    //beat miss: reset combo

    //attack tipp - normaler angriff
    //während combo beat auslassen - nächster normaler angriff ist salve
    //x gedückt halten, bis zu drei beatlängen - Aufladeangriff
    //x gedrückt halten und bewege input - aufgeladener bewegungsangriff

    //nötige Animationen:
    //schwach
    //normal 1,2,3,4,5 mit steigernem schaden/intensity
    //aufladeangriff, aufladeanimation, aufladeimpakt
    //aufgeladener bewegungsangriff
}