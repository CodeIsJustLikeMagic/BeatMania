using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

public enum WalkState {See_And_In_Range = 2, See_Not_In_Range = 1, Dont_See = 0}
/// <summary>
/// Alien WalkBehavior. Can get called by other AlienActions in order to make Alien walk around,
/// chase player, and stop when player is in range for an action
/// </summary>
public class WalkBehavior : MonoBehaviour
{
    [Tooltip("for target selection")]
    [SerializeField] private float vision_range = 8;
    [SerializeField] private float walking_speed = 1f;
    [SerializeField] private int turn_around_randomly_probability;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundForwardCheck;
    [SerializeField] private LayerMask m_WhatIsGround;

    [Tooltip("enable flying")]
    [SerializeField] private bool ignoreGroundCheck;
    
    private Animator anim;
    private Rigidbody rb;
    private static GameObject player;
    private float distancePlayer;
    private bool move = false;
    
    public void Stop()
    {
        StopToPerformAction();
    }
    
    /// <summary>
    /// Alien walks around, chases player and stops when enemy is in given action_range.
    /// </summary>
    /// <param name="action_range">distance in float from the player at which point the alien will stop chasing to perform their action</param>
    /// <param name="walk_if_not_in_range">disable walking around. Default: true</param>
    /// <param name="turn_to_player">disable turning toward the player. Dont set this to false if you want the alien to chase the palyer. Default: true</param>
    /// <returns>WalkState. Player is visible and in action_range, Player is visible(chase them) or Player is not visible(walk around)</returns>
    public WalkState CheckForEnemyInRange(float bps, float action_range, bool walk_if_not_in_range, bool turn_to_player)
    {
        if (CanSee(player))
        {
            if (turn_to_player)
            {
                RotateToPlayer();
                distancePlayer = Vector3.Distance(gameObject.transform.position,
                    player.transform.position);
                //Debug.Log("Distance to payer is "+distancePlayer);
            }
            if (distancePlayer <= action_range) // chase player
            {
                if (walk_if_not_in_range)
                {
                    StopToPerformAction();
                }
                return WalkState.See_And_In_Range; // can see and is in range
            }
            else
            {
                if (walk_if_not_in_range)
                {
                    Chase(bps);
                }
                return WalkState.See_Not_In_Range; // can see but is not in range
            }
        }
        else
        {
            if (walk_if_not_in_range)
            {
                WalkAround();
            }
            return WalkState.Dont_See; // cannot see player
        }
    }

    public WalkState CheckForEnemyInRange(float bps, float action_range)
    {
        return CheckForEnemyInRange(bps, action_range, true, true);
    }
    
    /// <summary>
    /// uses a raycast to see check if object is visible
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CanSee(GameObject player) //Raycast to see if we can hit
    {
        // only see player and ground
        var terrain_layer = 14;
        var player_layer = 9;

        var layermask1 = 1 << terrain_layer;
        var layermask2 = 1 << player_layer;
        
        var finalmask = layermask1 | layermask2; //only water and ignoreprojectiles
        //finalmask = ~finalmask; //everything except water and ignoreprojectiles

        Vector3 fromPosition = this.gameObject.transform.position;
        fromPosition.y = fromPosition.y + 1;
        Vector3 toPosition = player.transform.position;
        toPosition.y = toPosition.y + 1;
        Vector3 direction = toPosition - fromPosition;
        
        RaycastHit hit;
        if (Physics.Raycast(fromPosition, direction, out hit, vision_range, finalmask)){
            if(hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    private void RotateToPlayer()
    {
        Vector3 targetPosition = new Vector3( player.transform.position.x, 
            this.transform.position.y, 
            player.transform.position.z ) ;

        Vector3 us_to_target = targetPosition - transform.position;
        Vector3 lookDirection = transform.TransformDirection(Vector3.forward);

        if (Vector3.Dot(lookDirection, us_to_target) < 0)
        {
            TurnAround();
        }
        
        
        //this.transform.LookAt( targetPosition );// no. you need to scale instead
        //if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        //{
        //    transform.position += transform.forward* walking_speed* Time.deltaTime;
        //}
    }
    
    void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        anim = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D;
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (move)
        {
            if (slow)
            {
                rb.MovePosition(transform.position + transform.forward * (walking_speed/10 * Time.deltaTime));
            }
            else
            {
                rb.MovePosition(transform.position + transform.forward * (walking_speed * Time.deltaTime));
            }
        }
    }

    #region Walking/Animations
    private void Chase(float bps)
    {
        slow = false;
        if (GroundInFrontIsNotSave() || WalkedIntoWall())
        {
            move = false;
            anim.SetTrigger("Wait");//do idle animation
        }
        else
        {
            anim.SetTrigger("Walk");
            move = true;
        }
    }

    private void StopToPerformAction()
    {
        move = false;
        // dont set an Animation Trigger because the Action will do that.
    }

    private bool slow = false;
    public void KeepMoving()
    {
        slow = true;
        move = true;
    }

    public void StopMoving()
    {
        move = false;
    }

    private void WalkAround()
    {
        anim.SetTrigger("Walk");
        RandomTurn(); // turn around with a random_turn_probability
        if (WalkedIntoWall() || GroundInFrontIsNotSave()) // check if forward movement is valid
        {
            TurnAround();
        }
        move = true;
        slow = false;
    }

    private bool WalkedIntoWall()
    {
        Collider[] colliders = Physics.OverlapSphere(wallCheck.position, 0.2f, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log("overlap sphere wallcheck hit "+colliders[i].gameObject.name);
            if (colliders[i].gameObject != gameObject)
                return true;
        }

        return false;
    }

    private bool GroundInFrontIsNotSave()
    {
        if (ignoreGroundCheck)
        {
            return false;
        }
        Collider[] colliders = Physics.OverlapSphere(groundForwardCheck.position, 0.2f, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log("overlap sphere groundcheck hit "+colliders[i].gameObject.name);
            if (colliders[i].gameObject != gameObject)
                return false;// hit ground. we are save to move forward
        }
        return true;// didnt hit ground. we are not save
    }

    private void TurnAround()
    {
        transform.RotateAround(transform.transform.position, Vector3.up, 180);
        Flip();
    }
    
    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void RandomTurn()
    {
        if(Random.Range(0,100) < turn_around_randomly_probability)
        {
            TurnAround();
        }
    }
    

    #endregion
    //https://answers.unity.com/questions/296347/move-transform-to-target-in-x-seconds.html
    public IEnumerator MyMoveForward(float seconds)//doesnt look right. Doenst walk continually
    {
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            elapsedTime += Time.deltaTime;
            
            transform.position += transform.forward * walking_speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}

