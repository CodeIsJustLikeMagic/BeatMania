using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float life = 3;
    public bool isInvincible = false;
    //private bool isHitted = false;

    void FixedUpdate()
    {
        if (life <= 0)
        {
            //deathanim

            StartCoroutine(DestroyEnemy());
        }
    }

    public void ApplyDamage(float damage)
    {
        if (!isInvincible)
        {
            //float direction = damage / Mathf.Abs(damage);
            damage = Mathf.Abs(damage);
            //transform.GetComponent<Animator>().SetBool("Hit", true);
            life -= damage;
            //rb.velocity = Vector2.zero;
            //rb.AddForce(new Vector2(direction * 500f, 100f));
            StartCoroutine(HitTime());
        }
    }

    IEnumerator HitTime()
    {
        //isHitted = true;
        isInvincible = true;
        yield return new WaitForSeconds(0.1f);
        //isHitted = false;
        isInvincible = false;
    }

    IEnumerator DestroyEnemy()
    {
        //CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        //capsule.size = new Vector2(1f, 0.25f);
        //capsule.offset = new Vector2(0f, -0.8f);
        //capsule.direction = CapsuleDirection2D.Horizontal;
        //yield return new WaitForSeconds(0.25f);
        //rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
