using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum WalkState {See_And_In_Range = 2, See_Not_In_Range = 1, Dont_See = 0}
/// <summary>
/// Alien WalkBehavior. Can get called by other AlienActions in order to make Alien walk around,
/// chase player, and stop when player is in range for an action
/// </summary>
public class WalkBehavior : MonoBehaviour
{
    private static GameObject player;
    public void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        anim = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D;
    }
    [Tooltip("for target selection")]
    public float vision_range = 8;

    [SerializeField] private float walking_speed = 1f;
    //public float attack_range = 1.4f;

    private Animator anim;

    private float distancePlayer;
    private int state = 0;

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
                Debug.Log("Distance to payer is "+distancePlayer);
            }
            if (distancePlayer <= action_range) // chase playerv
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
    
    private void RotateToPlayer()
    {
        Vector3 targetPostition = new Vector3( player.transform.position.x, 
            this.transform.position.y, 
            player.transform.position.z ) ;
        this.transform.LookAt( targetPostition );
        //if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        //{
        //    transform.position += transform.forward* walking_speed* Time.deltaTime;
        //}
    }

    /// <summary>
    /// uses a raycast to see check if object is visible
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CanSee(GameObject player) //Raycast to see if we can hit
    {
        // see though some materials. here ignoreprojectiles and water. 
        var ignoreprojectiles = 11; 
        var water = 4;

        var layermask1 = 1 << ignoreprojectiles;
        var layermask2 = 1 << water;
        
        var finalmask = layermask1 | layermask2; //only water and ignoreprojectiles
        finalmask = ~finalmask; //everything except water and ignoreprojectiles

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

    private bool move = false;
    private void Update()
    {
        if (move)
        {
            transform.position += transform.forward * (walking_speed * Time.deltaTime);
        }
        
    }

    #region Walking/Animations
    private void Chase(float bps)
    {
        Debug.Log("Alien chasing ");
        move = true;
        anim.SetTrigger("Walk");
    }

    private void StopToPerformAction()
    {
        Debug.Log("Alien Stoping");
        move = false;
        // dont set an Animation Trigger because the Action will do that.
    }

    private void WalkAround()
    {
        Debug.Log("Alien Walk Around");
        move = false;
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

