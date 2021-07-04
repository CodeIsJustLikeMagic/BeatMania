using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    //public Animator playerAnimator; //animator is set though inspector in unity editor
    private Animator animator;

    //public Transform attackCheck;

    private bool canAttack = true;
    private bool entryAttack = false; // combo entry
    private bool fullyCharged = false;
    private bool windUp = false;
    private float chargeLevel = 0;
    private float maxCharge = 0;
    private float beatLen = 0;

    public string targetEntity = "Alien";

    //damage values
    private float dmgvalue_normal = 2;
    private float dmgvalue_weak = 1;
    private float dmgvalue_spin = 6;
    private float dmgvalue_stagger = 6;

    [SerializeField] private AttackPerformer attackPerformer2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        attackPerformer2D = GetComponentInChildren<AttackPerformer>();
    }

    public void SetBps(float bps)
    {
        //two beats to charge
        maxCharge = 1 / bps * 2;
    }

    //Input
    void AttackInput()
    {
        if (canAttack)
        {
            //press attack button
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.K))
            {
                if (BeatChecker.instance.IsInBeat())
                {
                    if (windUp)
                    {
                        StaggerAttack();
                    }
                    else if (entryAttack)
                    {
                        WindUp();
                    }
                    else
                    {
                        Attack();
                    }
                }
                else
                {
                    WeakAttack();
                }
            }
            else if ((Input.GetAxis("LT") > 0 || Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("RT") > 0 || Input.GetKeyDown(KeyCode.E)))
            {
                DashAttack();
            }
            //hold attack button
            else if ((Input.GetKey("joystick button 2") || Input.GetKey(KeyCode.K)) && entryAttack)
            {
                Charge();
            }
            //release attack button
            else if ((Input.GetKeyUp("joystick button 2") || Input.GetKeyUp(KeyCode.K)) && entryAttack)
            {
                SpinAttack();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        AttackInput();
    }

    void Attack()
    {
        BeatIndicatorFeedback.instance.Success();
        canAttack = false;
        animator.SetBool("IsAttacking", true);
        attackPerformer2D.Perform("normal_attack", dmgvalue_normal, false, targetEntity);
        entryAttack = true;
        StartCoroutine(AttackCooldown());
    }

    void WeakAttack()
    {
        BeatIndicatorFeedback.instance.Failed();
        canAttack = false;
        animator.SetBool("IsAttacking", true);
        attackPerformer2D.Perform("weak_attack", dmgvalue_weak, false, targetEntity);
        resetCombo();
        StartCoroutine(WeakAttackCooldown());
    }

    void Charge()
    {
        if (!fullyCharged)
        {
            chargeLevel += Time.deltaTime;
            if (maxCharge <= chargeLevel)
            {
                fullyCharged = true;
                attackPerformer2D.Perform("charge", 0, false, targetEntity, true);
            }
        }
    }

    void SpinAttack()
    {
        if (fullyCharged && BeatChecker.instance.IsInBeat())
        {
            BeatIndicatorFeedback.instance.Success();
            animator.SetBool("IsSpinning", true);
            attackPerformer2D.Perform("spin_attack", dmgvalue_spin, false, targetEntity);
        }
        else
        {
            BeatIndicatorFeedback.instance.Failed();
            animator.SetBool("IsAttacking", true);
            attackPerformer2D.Perform("weak_attack", dmgvalue_weak, false, targetEntity);
        }
        resetCombo();
        StartCoroutine(SpinDuration());
    }

    void WindUp()
    {
        BeatIndicatorFeedback.instance.Success();
        canAttack = false;
        animator.SetBool("IsWindUp", true);
        attackPerformer2D.Perform("wind_up", dmgvalue_normal, false, targetEntity);
        windUp = true;
        StartCoroutine(AttackCooldown());
    }

    void StaggerAttack()
    {
        BeatIndicatorFeedback.instance.Success();
        canAttack = false;
        animator.SetBool("IsStaggering", true);
        attackPerformer2D.Perform("stagger", dmgvalue_stagger, true, targetEntity);
        resetCombo();
        StartCoroutine(AttackCooldown());
    }

    void DashAttack()
    {
        if (fullyCharged && BeatChecker.instance.IsInBeat())
        {
            BeatIndicatorFeedback.instance.Success();
            attackPerformer2D.Perform("pierce", dmgvalue_spin, false, targetEntity);
        }
        else if (fullyCharged && !BeatChecker.instance.IsInBeat())
        {
            BeatIndicatorFeedback.instance.Failed();
            attackPerformer2D.Perform("nothing", 0, false, targetEntity);
        }
        resetCombo();
        StartCoroutine(SpinDuration());
    }

    void resetCombo()
    {
        fullyCharged = false;
        entryAttack = false;
        chargeLevel = 0;
        entryAttack = false;
        windUp = false;
    }

    //public void DoDamage(float damage)
    //{
    //    Collider[] collidersEnemies = Physics.OverlapSphere(attackCheck.position, 3f);
    //    for (int i = 0; i < collidersEnemies.Length; i++)
    //    {
    //        if (collidersEnemies[i].gameObject.tag == "Enemy")
    //        {
    //            collidersEnemies[i].gameObject.SendMessage("ApplyDamage", damage);
    //        }
    //    }
    //}

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        canAttack = true;
    }

    IEnumerator WeakAttackCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        canAttack = true;
    }

    IEnumerator SpinDuration()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}
