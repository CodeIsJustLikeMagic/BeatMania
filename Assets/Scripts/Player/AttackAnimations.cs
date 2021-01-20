using System.Collections;
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
        if (Input.GetKeyDown("r"))
        {
            //todo make sure we are inside collider of songtree
            SongchangeSystem.instance.SongChange();
        }
    }
    // Update is called once per frame
    void Update()
    {
        TestInput();
    }


    // Combosystem
    private int combocounter = 0;
    int maxCombo = 5;


    void Attack()
    {
        if (BeatChecker.instance.IsInBeat())
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
