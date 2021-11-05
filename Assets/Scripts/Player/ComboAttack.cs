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

    private float _bps = 0;
    //private float beatLen = 0;

    public string targetEntity = "Alien";

    //damage values
    private float dmgvalue_normal = 2;
    private float dmgvalue_weak = 1;
    private float dmgvalue_spin = 6;
    private float dmgvalue_stagger = 6;

    [SerializeField] private AttackPerformer attackPerformer2D;
    private CharacterController playerHp;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerHp = GetComponent<CharacterController>();
    }

    void Start()
    {
        attackPerformer2D = GetComponentInChildren<AttackPerformer>();
    }

    public void SetBps(float bps)
    {
        //two beats to charge
        maxCharge = 1 / bps * 2;
        _bps = bps;
    }

    public void SetAttack(bool mode) {
        canAttack = mode;
    }

    //Input
    void AttackInput()
    {
        if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.J))
        {
            Shield();
        }
        else if (Input.GetKeyUp("joystick button 3") || Input.GetKeyUp(KeyCode.J)) //doesnt attack only stops shielding
        {
            StopShield();
        }
        if (canAttack)
        {
            //press attack button
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.K))
            {
                if (BeatChecker.Instance.IsInBeat())
                {
                    if (shielded)
                    {
                        SpinAttack();
                        BeatChecker.Instance.IsInBeat("Player Attack SpinAttack");
                    }
                    else if (windUp)// set ture after WindUp()
                    {
                        StaggerAttack();
                        BeatChecker.Instance.IsInBeat("Player Attack StaggerAttack 3");
                    }
                    else if (entryAttack) // set true after Attack()
                    {
                        WindUp();
                        BeatChecker.Instance.IsInBeat("Player Attack WindUpAttack 2");
                    }
                    else
                    {
                        Attack();
                        BeatChecker.Instance.IsInBeat("Player Attack StrongAttack 1");
                    }
                }
                else
                {
                    WeakAttack();
                    BeatChecker.Instance.IsInBeat("Player Attack Weak Attack");
                }
                shielded = false;
            }
            else if ((Input.GetAxis("LT") > 0 || Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("RT") > 0 || Input.GetKeyDown(KeyCode.E)))
            {
                DashAttack();
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
        BeatIndicatorFeedback.Instance.Success();
        canAttack = false;
        animator.SetBool("IsAttacking", true);
        attackPerformer2D.Perform("normal_attack", dmgvalue_normal, false, targetEntity);
        entryAttack = true;
        StartCoroutine(AttackCooldown());
    }

    void WeakAttack()
    {
        BeatIndicatorFeedback.Instance.Failed();
        canAttack = false;
        animator.SetBool("IsAttacking", true);
        attackPerformer2D.Perform("weak_attack", dmgvalue_weak, false, targetEntity);
        resetCombo();
        StartCoroutine(WeakAttackCooldown());
    }

    void Charge()
    {
        Debug.Log("Charge");
        if (!fullyCharged)
        {
            if (chargeLevel == 0) // start charge animation right away
            {
                attackPerformer2D.Perform("charge", 0, false, targetEntity, true);
            }
            chargeLevel += Time.deltaTime;
            if (maxCharge <= chargeLevel)
            {
                fullyCharged = true;
            }
        }
    }

    private Coroutine _coroutine_shield;
    private bool shielded = false;
    void Shield()
    {
        //Debug.Log("Player create shield. InBeat? "+BeatChecker.Instance.IsInBeat()+" delta "+BeatChecker.Instance.IsInBeatDelta()+" beatlength "+BeatChecker.Instance.BeatLength());
        attackPerformer2D.Perform("strong_shield", 0, false, targetEntity, true);
        BeatChecker.Instance.IsInBeat("Player Shield");
        //fullyCharged = true;
        shielded = true;
        playerHp.shielded = true;
        _coroutine_shield = StartCoroutine(ShieldDuration());
    }

    void StopShield()
    {
        StopCoroutine(_coroutine_shield);
        Invoke("ActuallyStopShield", 0.1f); // shield persists for 0.1 seconds.
        // more lenient when you tap to shield "block" attacks
        // allows you to spin/dash when you let go of block key just before.
    }

    private void ActuallyStopShield()
    {
        attackPerformer2D.Perform("nothing", 0, false, targetEntity);
        playerHp.shielded = false;
        shielded = false;
    }

    void SpinAttack()
    {
        playerHp.shielded = true;
        BeatIndicatorFeedback.Instance.Success();
        animator.SetBool("IsSpinning", true);
        attackPerformer2D.Perform("spin_attack", dmgvalue_spin, true, targetEntity);
        resetCombo();
        StartCoroutine(SpinDuration());
    }

    void WindUp()
    {
        BeatIndicatorFeedback.Instance.Success();
        canAttack = false;
        animator.SetBool("IsWindUp", true);
        attackPerformer2D.Perform("wind_up", dmgvalue_normal, false, targetEntity);
        windUp = true;
        StartCoroutine(AttackCooldown());
    }

    void StaggerAttack()
    {
        BeatIndicatorFeedback.Instance.Success();
        canAttack = false;
        animator.SetBool("IsStaggering", true);
        attackPerformer2D.Perform("stagger", dmgvalue_stagger, true, targetEntity);
        resetCombo();
        StartCoroutine(AttackCooldown());
    }

    void DashAttack()
    {
        if (shielded && BeatChecker.Instance.IsInBeat("PlayerAttack Dash"))
        {
            BeatIndicatorFeedback.Instance.Success();
            attackPerformer2D.Perform("pierce", dmgvalue_spin, true, targetEntity);
        }
        else if (shielded && !BeatChecker.Instance.IsInBeat())
        {
            BeatIndicatorFeedback.Instance.Failed();
            attackPerformer2D.Perform("nothing", 0, false, targetEntity);
        }
        resetCombo();
        shielded = false;
        StartCoroutine(SpinDuration());
    }

    void resetCombo()
    {
        //fullyCharged = false;
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
        playerHp.shielded = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(1/_bps*2); // shield for 2 beats
        StopShield();
        Debug.Log("stop shield coroutine");
    }
}
