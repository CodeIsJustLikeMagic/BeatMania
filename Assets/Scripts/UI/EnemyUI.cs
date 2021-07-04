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
    private Text enemyNameText;

    [SerializeField] private Slider enemyHealthSlider;

    private bool oneHP = false;

    private GameObject target;
    private AlienHealthBehavior enemyHealth;
    [Tooltip("Pixel offset from the enemy target")]
    [SerializeField]
    private Vector3 screenOffset = new Vector3(0f, 30f, 0f);


    float characterControllerheight = 2f;
    private Transform targetTransform;
    private Renderer targetRenderer;
    private CanvasGroup _canvasGroup;
    private Vector3 targetPosition;

    public void SetTarget(GameObject _target, float height=2f)
    {
        if (_target == null)
        {
            Debug.LogError("Missing EnemyMakerManager target for EnemyUI");
            return;
        }

        target = _target;
        enemyHealth = target.GetComponent<AlienHealthBehavior>();

        targetTransform = this.target.GetComponent<Transform>();
        targetRenderer = this.target.GetComponentInChildren<Renderer>();
        
        if (height != null)
        {
            characterControllerheight = height;
        }
        enemyHealthSlider.maxValue = enemyHealth.health;
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
            enemyHealthSlider.value = enemyHealth.health;
        }

        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private bool visible = false;
    private void LateUpdate()
    {
        //if target is outside of view frustum dont show their UI.
        if (targetRenderer != null)
        {
            if (targetRenderer.isVisible && visible)
            {
                this._canvasGroup.alpha =  1f;
                // follow the target GameObject on screen.
                if (targetTransform != null)
                {
                    targetPosition = targetTransform.position;
                    targetPosition.y += characterControllerheight;
                    this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
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