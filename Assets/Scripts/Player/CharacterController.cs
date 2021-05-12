using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterController : BaseHealthBehavior
{
        [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_WallCheck;                             //Posicion que controla si el personaje toca una pared

        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Rigidbody m_Rigidbody;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private Vector3 velocity = Vector3.zero;
        private float limitFallSpeed = 25f; // Limit fall speed

        public bool canDoubleJump = true; //If player can double jump
        [SerializeField] private float m_DashForce = 25f;
        private bool canDash = true;
        private bool isDashing = false; //If player is dashing
        private bool m_IsWall = false; //If there is a wall in front of the player
        private bool hasJumped = false; //If player

        private bool isWallSliding = false; //If player is sliding in a wall
        private bool oldWallSlidding = false; //If player is sliding in a wall in the previous frame
        private float prevVelocityX = 0f;
        private bool canCheck = false; //For check if player is wallsliding

        public float life = 10f; //Life of the player
        private float maxlife;
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

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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


        //

        Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
            if (!wasGrounded)
            {
                OnLandEvent.Invoke();
                //if (!m_IsWall && !isDashing)
                //    particleJumpDown.Play();
                canDoubleJump = true;
                if (m_Rigidbody.velocity.y < 0f)
                    limitVelOnWallJump = false;
            }
        }

        m_IsWall = false;

        //
        if (!m_Grounded)
        {
            OnFallEvent.Invoke();
            Collider[] collidersWall = Physics.OverlapSphere(m_WallCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < collidersWall.Length; i++)
            {
                if (collidersWall[i].gameObject != null)
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
                //animator.SetBool("JumpUp", true);
                m_Grounded = false;
                m_Rigidbody.AddForce(new Vector2(0f, m_JumpForce));
                canDoubleJump = true;
                //particleJumpDown.Play();
                //particleJumpUp.Play();
            }
            //Doublejump
            else if (!m_Grounded && jump && canDoubleJump && !isWallSliding)
            {
                if (BeatChecker.instance.IsInBeat())
                {
                    canDoubleJump = false;
                    m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, 0);
                    m_Rigidbody.AddForce(new Vector2(0f, m_JumpForce / 1.2f));
                    animator.SetBool("IsDoubleJumping", true);
                    BeatIndicatorFeedback.instance.Success();
                }
                else
                {
                    BeatIndicatorFeedback.instance.Failed();
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
                if (jump && isWallSliding)
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

    IEnumerator DashCooldown()
    {
        animator.SetBool("IsDashing", true);
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

    public override void ApplyDamage(float damage, bool stagger, Vector3 position)
    {// implements Base Health Behavior. gets called when AttackPerformer hits something
        //Debug.Log("Player apply damage");
        if (!invincible)
        {
            animator.SetBool("Hit", true);
            life -= damage;
            Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
            m_Rigidbody.velocity = Vector2.zero;
            m_Rigidbody.AddForce(damageDir * 4);
            if (life <= 0)
            {
                StartCoroutine(WaitToDead());
            }
            else
            {
                //StartCoroutine(Stun(0.25f));
                //StartCoroutine(MakeInvincible(1f)); // invincible is not really nessesary
            }
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
        animator.SetBool("IsDead", true);
        canMove = false;
        invincible = true;
        //GetComponent<Attack>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
        yield return new WaitForSeconds(1.1f);
        
        //Behavior for when player dies
        transform.position = Checkpoint.getSpwanPosition();
        animator.SetBool("IsDead", false);
        life = maxlife;
        canMove = true;
        invincible = false;
    }

    //    public void Move(float move, bool jump, bool dashLEFT, bool dashRIGHT)
    //    {
    //        if (canMove)
    //        {
    //            if ((dashLEFT || dashRIGHT) && canDash && !isWallSliding)
    //            {
    //                //m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, 0f));
    //                StartCoroutine(DashCooldown());
    //            }
    //            // If crouching, check to see if the character can stand up
    //            if (isDashing)
    //            {
    //                if (dashLEFT)
    //                {
    //                    if (m_FacingRight)
    //                    {
    //                        Flip();
    //                    }
    //                    m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
    //                }
    //                else if (dashRIGHT)
    //                {
    //                    if (!m_FacingRight)
    //                    {
    //                        Flip();
    //                    }
    //                    m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
    //                }
    //            }
    //            //only control the player if grounded or airControl is turned on
    //            else if (m_Grounded || m_AirControl)
    //            {
    //                if (m_Rigidbody2D.velocity.y < -limitFallSpeed)
    //                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -limitFallSpeed);
    //                // Move the character by finding the target velocity
    //                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
    //                // And then smoothing it out and applying it to the character
    //                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

    //                // If the input is moving the player right and the player is facing left...
    //                if (move > 0 && !m_FacingRight && !isWallSliding)
    //                {
    //                    // ... flip the player.
    //                    Flip();
    //                }
    //                // Otherwise if the input is moving the player left and the player is facing right...
    //                else if (move < 0 && m_FacingRight && !isWallSliding)
    //                {
    //                    // ... flip the player.
    //                    Flip();
    //                }
    //            }
    //            // If the player should jump...
    //            if (m_Grounded && jump)
    //            {
    //                // Add a vertical force to the player.
    //                animator.SetBool("IsJumping", true);
    //                animator.SetBool("JumpUp", true);
    //                m_Grounded = false;
    //                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
    //                canDoubleJump = true;
    //                particleJumpDown.Play();
    //                particleJumpUp.Play();
    //            }
    //            //modified
    //            else if (!m_Grounded && jump && canDoubleJump && !isWallSliding && InBeat())
    //            {
    //                canDoubleJump = false;
    //                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
    //                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce / 1.2f));
    //                animator.SetBool("IsDoubleJumping", true);
    //            }

    //            else if (m_IsWall && !m_Grounded)
    //            {
    //                if (!oldWallSlidding && m_Rigidbody2D.velocity.y < 0 || isDashing)
    //                {
    //                    isWallSliding = true;
    //                    m_WallCheck.localPosition = new Vector3(-m_WallCheck.localPosition.x, m_WallCheck.localPosition.y, 0);
    //                    Flip();
    //                    StartCoroutine(WaitToCheck(0.1f));
    //                    canDoubleJump = true;
    //                    animator.SetBool("IsWallSliding", true);
    //                }
    //                isDashing = false;

    //                if (isWallSliding)
    //                {
    //                    if (move * transform.localScale.x > 0.1f)
    //                    {
    //                        StartCoroutine(WaitToEndSliding());
    //                    }
    //                    else
    //                    {
    //                        oldWallSlidding = true;
    //                        m_Rigidbody2D.velocity = new Vector2(-transform.localScale.x * 2, -5);
    //                    }
    //                }

    //                //modified
    //                if (jump && isWallSliding)
    //                {
    //                    animator.SetBool("IsJumping", true);
    //                    animator.SetBool("JumpUp", true);
    //                    m_Rigidbody2D.velocity = new Vector2(0f, 0f);
    //                    m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_JumpForce * 1.2f, m_JumpForce));
    //                    jumpWallStartX = transform.position.x;
    //                    limitVelOnWallJump = true;
    //                    canDoubleJump = true;
    //                    isWallSliding = false;
    //                    animator.SetBool("IsWallSliding", false);
    //                    oldWallSlidding = false;
    //                    m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
    //                    canMove = false;
    //                }
    //                else if ((dashLEFT || dashRIGHT) && canDash)
    //                {
    //                    isWallSliding = false;
    //                    animator.SetBool("IsWallSliding", false);
    //                    oldWallSlidding = false;
    //                    m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
    //                    canDoubleJump = true;
    //                    StartCoroutine(DashCooldown());
    //                }
    //            }
    //            else if (isWallSliding && !m_IsWall && canCheck)
    //            {
    //                isWallSliding = false;
    //                animator.SetBool("IsWallSliding", false);
    //                oldWallSlidding = false;
    //                m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
    //                canDoubleJump = true;
    //            }
    //        }
    //    }

    //    public void ApplyDamage(float damage, Vector3 position)
    //    {
    //        if (!invincible)
    //        {
    //            animator.SetBool("Hit", true);
    //            life -= damage;
    //            Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
    //            m_Rigidbody2D.velocity = Vector2.zero;
    //            m_Rigidbody2D.AddForce(damageDir * 10);
    //            if (life <= 0)
    //            {
    //                StartCoroutine(WaitToDead());
    //            }
    //            else
    //            {
    //                StartCoroutine(Stun(0.25f));
    //                StartCoroutine(MakeInvincible(1f));
    //            }
    //        }
    //    }

    //IEnumerator DashCooldown()
    //{
    //    animator.SetBool("IsDashing", true);
    //    isDashing = true;
    //    canDash = false;
    //    yield return new WaitForSeconds(0.1f);
    //    isDashing = false;
    //    yield return new WaitForSeconds(0.5f);
    //    canDash = true;
    //}

    //    IEnumerator Stun(float time)
    //    {
    //        canMove = false;
    //        yield return new WaitForSeconds(time);
    //        canMove = true;
    //    }
    //    IEnumerator MakeInvincible(float time)
    //    {
    //        invincible = true;
    //        yield return new WaitForSeconds(time);
    //        invincible = false;
    //    }
    //    IEnumerator WaitToMove(float time)
    //    {
    //        canMove = false;
    //        yield return new WaitForSeconds(time);
    //        canMove = true;
    //    }

    //    IEnumerator WaitToCheck(float time)
    //    {
    //        canCheck = false;
    //        yield return new WaitForSeconds(time);
    //        canCheck = true;
    //    }

    //    IEnumerator WaitToEndSliding()
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        canDoubleJump = true;
    //        isWallSliding = false;
    //        animator.SetBool("IsWallSliding", false);
    //        oldWallSlidding = false;
    //        m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
    //    }

    //    IEnumerator WaitToDead()
    //    {
    //        animator.SetBool("IsDead", true);
    //        canMove = false;
    //        invincible = true;
    //        //GetComponent<Attack>().enabled = false;
    //        yield return new WaitForSeconds(0.4f);
    //        m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
    //        yield return new WaitForSeconds(1.1f);
    //        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    //    }

    //    ///

    //    //Hold Beat
    //    float beatStart = 0;
    //    float beatLength = 0;
    //    float toleranceRange = 0.1f;//

    //    bool InBeat()//is current time in beat?
    //    {
    //        float now = Time.time;
    //        //now is in beat if: now == beatStart + n * beatLegnth +- toleranceRange; where n is any natural number
    //        float missedBySeconds = (now - beatStart) % beatLength; //missed the beat by seconds.miliseconds. 
    //        //allways oriented toward the next comming beat. 
    //        //so if you hit the beat within the tolerance range its either almost beatLength, or almost 0 
    //        Debug.Log("InBeat " + missedBySeconds + " ranges: 0-" + toleranceRange + "; " + (beatLength - toleranceRange) + "-" + beatLength);
    //        return missedBySeconds <= toleranceRange || missedBySeconds >= beatLength - toleranceRange;
    //    }

    //    public void OnBeat(float bps)
    //    {
    //        beatStart = Time.time;
    //        beatLength = 1 / bps;
    //    }

}