using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CharacterController : BaseHealthBehavior
{
    private static CharacterController _instance;

    public static CharacterController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharacterController>();
                if (_instance == null)
                {
                    Debug.LogError("Couldnt find instance of CharacterController");
                }
            }

            return _instance;
        }
    }
    
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [SerializeField] private float m_FallMultiplier = 2.5f;                     // Gravity multiplier on Player when falling.
    [SerializeField] private float m_LowJumpMultiplier = 2f;                  // Gravity multiplier on Player low-Jump.
    [SerializeField] private float limitFallSpeed = 25f; // Limit fall speed
    private bool isLowJumping = false;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround =0;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck = null;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_WallCheck = null;                             //Posicion que controla si el personaje toca una pared

    [SerializeField] private ParticleSystem jumpParticleSystem  = null;
    [SerializeField] private ParticleSystem jumpFailParticleSystem = null;
    [SerializeField] private ParticleSystem dashParticleSystem = null;
    
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody m_Rigidbody;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;

    public bool canDoubleJump = true; //If player can double jump
    [SerializeField] private float m_DashForce = 25f;
    private bool canDash = true;
    private bool isDashing = false; //If player is dashing
    private bool m_IsWall = false; //If there is a wall in front of the player
    //private bool hasJumped = false; //If player

    public bool touchedRightWall = false;  //Stores Information if Player jumped from a right wall
    public bool touchedLeftWall = false;   //Stores Information if Player jumped from a left  wall

    private bool isWallSliding = false; //If player is sliding in a wall
    private bool oldWallSlidding = false; //If player is sliding in a wall in the previous frame
    private float prevVelocityX = 0f;
    private bool canCheck = false; //For check if player is wallsliding

    public float life = 10f; //Life of the player
    public float maxlife;
    public bool invincible = false; //If player can die
    private bool canMove = true; //If player can move

    private Animator animator;
    //    public ParticleSystem particleJumpUp; //Trail particles
    //    public ParticleSystem particleJumpDown; //Explosion particles

    private float jumpWallStartX = 0;
    private float jumpWallDistX = 0; //Distance between player and wall
    private bool limitVelOnWallJump = false; //For limit wall jump distance with low fps

    [Header("Events")]
    [Space]

    public UnityEvent OnFallEvent;
    public UnityEvent OnLandEvent;

    [SerializeField]
    private ComboAttack combo  = null;
    [SerializeField]
    private DamageFeedback feedback  = null;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        //instance = this;
        m_Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        combo = gameObject.GetComponent<ComboAttack>();
        feedback = gameObject.GetComponent<DamageFeedback>();

        if (OnFallEvent == null)
            OnFallEvent = new UnityEvent();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        Checkpoint.setFirstSpawnPosition(transform.position); // when the player dies before finding the first checkpoint they respawn where they started the level
        maxlife = life;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.

        //Groundcheck
        Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "GroundandWall" || colliders[i].gameObject.tag == "Ground")
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    //if (!m_IsWall && !isDashing)
                    //    particleJumpDown.Play();
                    //canDoubleJump = true;
                    touchedLeftWall = false;
                    touchedRightWall = false;
                    if (m_Rigidbody.velocity.y < 0f)
                        limitVelOnWallJump = false;
                }
            }
        }

        m_IsWall = false;

        //Wallcheck
        if (!m_Grounded)
        {
            OnFallEvent.Invoke();
            Collider[] collidersWall = Physics.OverlapSphere(m_WallCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < collidersWall.Length; i++)
            {
                if (collidersWall[i].gameObject.tag == "GroundandWall")
                {
                    isDashing = false;
                    m_IsWall = true;
                }
            }
            prevVelocityX = m_Rigidbody.velocity.x;
        }


        //
        if (limitVelOnWallJump)
        {
            if (m_Rigidbody.velocity.y < -0.5f)
                limitVelOnWallJump = false;
            jumpWallDistX = (jumpWallStartX - transform.position.x) * transform.localScale.x;
            if (jumpWallDistX < -0.5f && jumpWallDistX > -1f)
            {
                canMove = true;
            }
            else if (jumpWallDistX < -1f && jumpWallDistX >= -2f)
            {
                canMove = true;
                m_Rigidbody.velocity = new Vector2(10f * transform.localScale.x, m_Rigidbody.velocity.y);
            }
            else if (jumpWallDistX < -2f)
            {
                limitVelOnWallJump = false;
                m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
            }
            else if (jumpWallDistX > 0)
            {
                limitVelOnWallJump = false;
                m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
            }
        }

        //jump improvements
        if (m_Rigidbody.velocity.y < 0)
        {
            isLowJumping = false;
            m_Rigidbody.velocity += Vector3.up * Physics.gravity.y * (m_FallMultiplier - 1) * Time.deltaTime;
        }
        //else if ((m_Rigidbody.velocity.y > 0 && isLowJumping) || (m_Rigidbody.velocity.y > 0 && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))))
        else if ((m_Rigidbody.velocity.y > 0 && isLowJumping))
        {
            m_Rigidbody.velocity += Vector3.up * Physics.gravity.y * (m_LowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Move(float move, bool jump, bool dashLeft, bool dashRight)
    {
        if (canMove)
        {
            //Dash
            if ((dashLeft || dashRight) && canDash && !isWallSliding)
            {
                StartCoroutine(DashCooldown());
            }
            if (isDashing)
            {
                if (dashLeft)
                {
                    if (m_FacingRight)
                    {
                        Flip();
                    }
                    m_Rigidbody.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
                }
                else if (dashRight)
                {
                    if (!m_FacingRight)
                    {
                        Flip();
                    }
                    m_Rigidbody.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
                }
            }

            //Bewegung
            else if (m_Grounded || m_AirControl)
            {
                if (m_Rigidbody.velocity.y < -limitFallSpeed)
                    m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, -limitFallSpeed);
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight && !isWallSliding)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight && !isWallSliding)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            //Jump
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                animator.SetBool("IsJumping", true);
                if (BeatChecker.Instance.IsInBeat())
                {
                    canDoubleJump = true;
                    BeatIndicatorFeedback.instance.Success();
                    jumpParticleSystem.Play();
                }
                else
                {
                    isLowJumping = true;
                    BeatIndicatorFeedback.instance.Failed();
                }
                m_Grounded = false;
                m_Rigidbody.AddForce(new Vector2(0f, m_JumpForce));
                
            }
            //Doublejump
            else if (!m_Grounded && jump && canDoubleJump && !isWallSliding && !isLowJumping)
            {
                if (BeatChecker.Instance.IsInBeat())
                {
                    canDoubleJump = false;
                    m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, 0);
                    m_Rigidbody.AddForce(new Vector2(0f, m_JumpForce / 1.2f));
                    animator.SetBool("IsDoubleJumping", true);
                    BeatIndicatorFeedback.instance.Success();
                    jumpParticleSystem.Play();
                }
                else
                {
                    BeatIndicatorFeedback.instance.Failed();
                    jumpFailParticleSystem.Play();
                }
            }
            else if (m_IsWall && !m_Grounded)
            {
                if (!oldWallSlidding && m_Rigidbody.velocity.y < 0 || isDashing)
                {
                    isWallSliding = true;
                    m_WallCheck.localPosition = new Vector3(-m_WallCheck.localPosition.x, m_WallCheck.localPosition.y, 0);
                    Flip();
                    StartCoroutine(WaitToCheck(0.1f));
                    canDoubleJump = true;
                    animator.SetBool("IsWallSliding", true);
                }

                isDashing = false;

                if (isWallSliding)
                {
                    if (move * transform.localScale.x > 0.1f)
                    {
                        StartCoroutine(WaitToEndSliding());
                    }
                    else
                    {
                        oldWallSlidding = true;
                        m_Rigidbody.velocity = new Vector2(-transform.localScale.x * 2, -5);
                    }
                }
                //modified
                if (jump && isWallSliding && ((m_FacingRight && !touchedLeftWall) ||(!m_FacingRight && !touchedRightWall)))
                {
                    animator.SetBool("IsJumping", true);
                    animator.SetBool("JumpUp", true);
                    m_Rigidbody.velocity = new Vector2(0f, 0f);
                    m_Rigidbody.AddForce(new Vector2(transform.localScale.x * m_JumpForce * 1.2f, m_JumpForce));
                    jumpWallStartX = transform.position.x;
                    limitVelOnWallJump = true;
                    canDoubleJump = true;
                    isWallSliding = false;
                    animator.SetBool("IsWallSliding", false);
                    oldWallSlidding = false;
                    m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
                    canMove = false;
                    if (m_FacingRight)
                    {
                        touchedLeftWall = true;
                        touchedRightWall = false;
                    }
                    else {
                        touchedLeftWall = false;
                        touchedRightWall = true;
                    }
                }
                else if ((dashLeft || dashRight) && canDash)
                {
                    isWallSliding = false;
                    animator.SetBool("IsWallSliding", false);
                    oldWallSlidding = false;
                    m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
                    canDoubleJump = true;
                    StartCoroutine(DashCooldown());
                }
            }
            else if (isWallSliding && !m_IsWall && canCheck)
            {
                isWallSliding = false;
                animator.SetBool("IsWallSliding", false);
                oldWallSlidding = false;
                m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
                canDoubleJump = true;
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator StaggerTime(float time = 0.5f) {
        canMove = false;
        canDash = false;
        combo.SetAttack(false);
        yield return new WaitForSeconds(time);
        feedback.doNotDisplayDamage();
        canMove = true;
        canDash = true;
        combo.SetAttack(true);
    }

    IEnumerator DashCooldown()
    {
        animator.SetBool("IsDashing", true);
        dashParticleSystem.Play();
        isDashing = true;
        canDash = false;
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }

    IEnumerator WaitToCheck(float time)
    {
        canCheck = false;
        yield return new WaitForSeconds(time);
        canCheck = true;
    }

    IEnumerator WaitToEndSliding()
    {
        yield return new WaitForSeconds(0.1f);
        canDoubleJump = true;
        isWallSliding = false;
        animator.SetBool("IsWallSliding", false);
        oldWallSlidding = false;
        m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
    }

    public override void ApplyDamage(float damage, bool stagger, Vector3 position, float forceMulti= 1f)
    {// implements Base Health Behavior. gets called when AttackPerformer hits something
        //Debug.Log("Player apply damage");
        if (!invincible)
        {
            animator.SetBool("Hit", true);
            life -= damage;
            Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
            m_Rigidbody.velocity = Vector2.zero;
            m_Rigidbody.AddForce(damageDir * forceMulti);
            if (life <= 0)
            {
                StartCoroutine(WaitToDead());
            }
            else
            {
                StartCoroutine(StaggerTime());
            }
        }
    }

    public override void AddForce(Vector3 position, float forceMulti)
    {
        Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.AddForce(damageDir * forceMulti);
    }

    public override void ApplyHeal(float dmg)
    {
        life += dmg;
        if (life > maxlife) // make sure we cant go beyond maxhealth
        {
            life = maxlife;
        }
    }

    IEnumerator MakeInvincible(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    IEnumerator WaitToDead()
    {
        combo.SetAttack(false);
        canMove = false;
        invincible = true;
        yield return new WaitForSeconds(0.5f);
        feedback.doNotDisplayDamage();
        animator.SetBool("IsDead", true);
        m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
        yield return new WaitForSeconds(1.1f);

        //Behavior for when player dies
        transform.position = Checkpoint.getSpwanPosition();
        animator.SetBool("IsDead", false);
        life = maxlife;
        combo.SetAttack(true);
        canMove = true;
        invincible = false;
    }


    public void DebugTeleport(Vector3 position)
    {
        transform.position = position;
    }

    public void SetyPosition(float f)
    {
        Vector3 v = new Vector3(transform.position.x, f, transform.position.z);
        transform.position = v;
    }
}