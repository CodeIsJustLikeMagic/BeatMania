using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class just does almost everything right now
public class PlayerAttack : MonoBehaviour
{
    //public Animator playerAnimator; //animator is set though inspector in unity editor

    public bool canAttack = true;
    public Transform attackCheck;

    [SerializeField] private AttackPerformer attackPerformer2D;

    void Start()
    {
        attackPerformer2D = GetComponentInChildren<AttackPerformer>();
    }
    //Input
    void TestInput()
    {
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.K))
        {
            if (canAttack)
            {
                Attack();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        TestInput();
    }

    // Combosystem
    private float combocounter = 0;
    int maxCombo = 5;


    void Attack()
    {
        canAttack = false;
        if (BeatChecker.instance.IsInBeat())
        {
            //playerAnimator.SetTrigger("TAN" + (combocounter + 1)); // Test Attack Normal 1-5
            combocounter = (combocounter + 1) % maxCombo;
            attackPerformer2D.Perform("Attack" + (combocounter + 1), combocounter, false);
            //DoDamage(combocounter);
        }
        else
        {
            combocounter = 1; //reset combo
            attackPerformer2D.Perform("whiteN1", combocounter, false); // do weak attack
            //DoDamage(combocounter);
        }
        StartCoroutine(AttackCooldown());
    }

    public void DoDamage(float damage) {
        Collider[] collidersEnemies = Physics.OverlapSphere(attackCheck.position, 3f);
        for (int i = 0; i < collidersEnemies.Length; i++)
        {
            if (collidersEnemies[i].gameObject.tag == "Enemy")
            {
                collidersEnemies[i].gameObject.SendMessage("ApplyDamage", damage);
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        canAttack = true;
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
