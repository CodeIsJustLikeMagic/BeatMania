using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class AlienHealthBehavior : BaseHealthBehavior
{
    [field: SerializeField] public float health { get; private set; } = 10f;
    [SerializeField] private float deathAnimationLength = 2f;
    [Tooltip("For each song: Set if we can take damage or not")]
    [SerializeField]
    private bool[] canTakeDamage = {true, true, true};

    private SkinnedMeshRenderer visual;
    private Collider collider;

    private float maxHealth = 10f;
    private AttackBehavior _attackBehavior;
    private AlienHandleSongChange _alienHandleSongChange;

    private bool _vulnerable = true;
    private Vector3 _respawnLocation;

    public GameObject UIPrefab;
    [SerializeField] private float height;
    [SerializeField] EnemyUI enemyUi;
    void Awake()
    {
        maxHealth = health;
        _attackBehavior = GetComponent<AttackBehavior>();
        _alienHandleSongChange = GetComponent<AlienHandleSongChange>();
        visual = GetComponentInChildren<SkinnedMeshRenderer>();
        collider = GetComponent<Collider>();
        _respawnLocation = transform.position;
        
    }
    public void OnSongChange(int song)
    {
        // revive if we were dead.
        if (is_dead)
        {
            Revive();
        }
        _vulnerable = canTakeDamage[song % canTakeDamage.Length];
        if (enemyUi == null)
        {
            GameObject _uiGo = Instantiate(UIPrefab);
            //_uiGo.SendMessage("SetTarget", this.gameObject, SendMessageOptions.RequireReceiver);
            enemyUi = _uiGo.GetComponent<EnemyUI>();
            enemyUi.SetTarget(this.gameObject, height);
        }
        enemyUi.SetVisible(_vulnerable);
    }

    private bool is_dead = false;
    private void Die()
    {
        is_dead = true;
        visual.enabled = false;
        collider.enabled = false;
        enemyUi.SetVisible(false);
    }

    private void Revive()
    {
        is_dead = false;
        health = maxHealth;
        _vulnerable = true;
        _alienHandleSongChange.is_dead = false;
        visual.enabled = true;
        collider.enabled = true;
        transform.position = _respawnLocation;
    }

    public override void ApplyDamage(float dmg, bool stagger, Vector3 pos)
    {
        if (_vulnerable)
        {
            health -= dmg;
            if (health <= 0.3)
            {
                Debug.Log("Die");
                //Play death animation
                gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D.SetTrigger("Die");
                //float bps = ((AudioHelmClock)FindObjectOfType(typeof(AudioHelmClock))).bpm / 60;
                // not sure if death animation should be in beat
                _alienHandleSongChange.is_dead = true; //stop attackbehavior from starting attacks
                Invoke("Die", deathAnimationLength);
                _vulnerable = false; // dont take damage until we die. Stops us from reseting Die animation.
            }
            else
            {
                _attackBehavior.Stagger();
            }
        }
    }

    public override void ApplyHeal(float dmg)
    {
        throw new NotImplementedException();
    }
}
