using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimations : MonoBehaviour
{
    public Animator playerAnimator; //animator is set though inspector in unity editor
    // Start is called before the first frame update

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
       beatStart = FakeBeat.instance.beatStart;
       beatLength = FakeBeat.instance.beatLength;
    }

    bool InBeat()//is current time in beat?
    {
        float now = Time.time;
        //now is in beat if: now == beatStart + n * beatLegnth +- toleranceRange; where n is any natural number
        float missedBySeconds = (now - beatStart) % beatLength; //missed the beat by seconds.miliseconds. 
        //allways oriented toward the next comming beat. 
        //so if you hit the beat within the tolerance range its either almost beatLength, or almost 0 
        Debug.Log("InBeat " + missedBySeconds+ " ranges: 0-"+toleranceRange+"; "+(beatLength-toleranceRange)+"-"+beatLength);
        return missedBySeconds <= toleranceRange || missedBySeconds >= beatLength-toleranceRange;
        
    }

    // Combosystem
    private int combocounter = 0;
    
    void Attack()
    {
        if (InBeat())
        {
            DoAttackStrong();
        }
        else DoAttackWeak();
    }

    void DoAttackStrong()
    {
        playerAnimator.SetTrigger("blueN1");
    }
    void DoAttackWeak()
    {
        playerAnimator.SetTrigger("whiteN1");
    }
    
}
