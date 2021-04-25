using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class AlienHealthBehavior : BaseHealthBehavior
{
    [SerializeField] private float Health = 10f;
    [SerializeField] private float deathAnimationLength = 2f;
    [Tooltip("For each song: Set if we can take damage or not")]
    [SerializeField]
    private bool[] canTakeDamage = {true, true, true};

    private AttackBehavior _attackBehavior;

    private bool _vulnerable = true;

    void Start()
    {
        _attackBehavior = GetComponent<AttackBehavior>();
    }
    public void OnSongChange(int song)
    {
        _vulnerable = canTakeDamage[song % canTakeDamage.Length];
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public override void ApplyDamage(float dmg, bool stagger, Vector3 pos)
    {
        if (_vulnerable)
        {
            Health -= dmg;
            if (Health <= 0.3)
            {
                Debug.Log("Die");
                //Play death animation
                gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D.SetTrigger("Die");
                //float bps = ((AudioHelmClock)FindObjectOfType(typeof(AudioHelmClock))).bpm / 60;
                // not sure if death animation should be in beat
                _attackBehavior.suppress = true; //stop attackbehavior from starting attacks
                Invoke("Die", deathAnimationLength);
                _vulnerable = false; // dont take damage until we die. Stops us from reseting Die animation.
            }
            else
            {
                _attackBehavior.Stagger();
            }
        }
    }
}
