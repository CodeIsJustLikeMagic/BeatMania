using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;
using UnityEngine.Serialization;
using Random = System.Random;

public class AlienHealthBehavior : BaseHealthBehavior
{
    [field: SerializeField] public float health { get; private set; } = 10f;
    public Renderer UItargetRenderer;

    [SerializeField] private float deathAnimationLength = 2f;
    [Tooltip("For each song: Set if we can take damage or not")]
    [SerializeField]
    private bool[] canTakeDamage = {true, true, true};

    private SkinnedMeshRenderer _visual;
    private Collider _collider;

    private float _maxHealth = 10f;
    private AttackBehavior _attackBehavior;
    private AlienHandleSongChange _alienHandleSongChange;

    private bool _vulnerable = true;
    private Vector3 _respawnLocation;

    [FormerlySerializedAs("UIPrefab")] public GameObject uiPrefab;
    [SerializeField] private float height = 0;
    [SerializeField] EnemyUI enemyUi;
    void Awake()
    {
        _maxHealth = health;
        _attackBehavior = GetComponent<AttackBehavior>();
        _alienHandleSongChange = GetComponent<AlienHandleSongChange>();
        _visual = GetComponentInChildren<SkinnedMeshRenderer>();
        _collider = GetComponent<Collider>();
        _respawnLocation = transform.position;
        UItargetRenderer = _visual;
        
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
            GameObject _uiGo = Instantiate(uiPrefab);
            //_uiGo.SendMessage("SetTarget", this.gameObject, SendMessageOptions.RequireReceiver);
            enemyUi = _uiGo.GetComponent<EnemyUI>();
            enemyUi.SetTarget(this.gameObject, height);
        }
        else
        {
            transform.position = _respawnLocation;
        }
        enemyUi.SetVisible(_vulnerable);
        if (_vulnerable)
        {
            GetComponent<Rigidbody>().mass = 1000;
        }
        else
        {
            GetComponent<Rigidbody>().mass = 10000;
        }
    }

    private bool is_dead = false;

    private void DieStart()
    {
        //Play death animation
        gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D.SetTrigger("Die");
        //float bps = ((AudioHelmClock)FindObjectOfType(typeof(AudioHelmClock))).bpm / 60;
        // not sure if death animation should be in beat
        _alienHandleSongChange.is_dead = true; //stop attackbehavior from starting attacks
        _collider.enabled = false;
        enemyUi.SetVisible(false);
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<WalkBehavior>().StopMoving();
        _vulnerable = false; // dont take damage until we die. Stops us from reseting Die animation.
    }
    private void DieEnd()
    {
        is_dead = true;
        _visual.enabled = false;
    }

    private void Revive()
    {
        is_dead = false;
        health = _maxHealth;
        _vulnerable = true;
        _alienHandleSongChange.is_dead = false;
        _visual.enabled = true;
        _collider.enabled = true;
        transform.position = _respawnLocation;
        GetComponent<Rigidbody>().useGravity = true;
        _alienHandleSongChange.enemyAnimator3D.SetTrigger("Wait");
    }

    public override void ApplyDamage(float dmg, bool stagger, Vector3 pos, float forceMulti= 4f)
    {
        if (_vulnerable)
        {
            health -= dmg;
            if (health <= 0.3)
            {
                Debug.Log("Die");
                DieStart();
                Invoke("DieEnd", deathAnimationLength);
                
            }
            else if(stagger)
            {
                _alienHandleSongChange.skip = 2; // get locked out of acting for 2 beats. is this to much?
                _attackBehavior.Stagger();
            }
        }
    }

    public override void ApplyHeal(float dmg)
    {
        throw new NotImplementedException();
    }

    public override void AddForce(Vector3 position, float forceMulti)
    {
        throw new NotImplementedException();
    }
}
