using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider enemyHealthSlider = null;
    
    [Tooltip("Pixel offset from the enemy target")]
    [SerializeField]
    private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

    public GameObject _target;
    private AlienHealthBehavior _enemyHealth;
    private float _characterControllerHeight = 2f;
    private Transform _targetTransform;
    public Renderer _targetRenderer;
    private CanvasGroup _canvasGroup;
    private Vector3 _targetPosition;

    public void SetTarget(GameObject _target, float height=2f)
    {
        if (_target == null)
        {
            Debug.LogError("Missing EnemyMakerManager target for EnemyUI");
            return;
        }

        this._target = _target;
        _enemyHealth = this._target.GetComponent<AlienHealthBehavior>();

        _targetTransform = this._target.GetComponent<Transform>();
        _targetRenderer = this._target.GetComponentInChildren<SkinnedMeshRenderer>();
        if (_targetRenderer == null)
        {
            Debug.LogError("UI without Renderer");
        }
        _characterControllerHeight = height;
        enemyHealthSlider.maxValue = _enemyHealth.health;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
}

    // Update is called once per frame
    void Update()
    {
        if (enemyHealthSlider != null) // allways keeps the same health as the health component
        {
            enemyHealthSlider.value = _enemyHealth.health;
        }

        if (_target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private bool visible = false;
    private void LateUpdate()
    {
        //if target is outside of view frustum dont show their UI.
        if (_targetRenderer != null)
        {
            Debug.Log("target name "+_target.name+" isVisible "+_targetRenderer.isVisible+" visible "+visible,_targetRenderer);
            if (_targetRenderer.isVisible && visible)
            {
                this._canvasGroup.alpha =  1f;
                // follow the target GameObject on screen.
                if (_targetTransform != null)
                {
                    _targetPosition = _targetTransform.position;
                    _targetPosition.y += _characterControllerHeight;
                    this.transform.position = Camera.main.WorldToScreenPoint(_targetPosition) + screenOffset;
                }
            }
            else
            {
                this._canvasGroup.alpha =  0f;
            }
            
        }

    }

    public void SetVisible(bool visible)
    {
        this.visible = visible;
    }
}