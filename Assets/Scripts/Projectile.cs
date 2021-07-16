using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage;
    //public GameObject destroyEffect;
    public Vector2 forward;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    //  Debug.Log("enemy Must Take Damage!");
                    //  hitInfo.collider.GetComponent<BossEnemy>(); //.TakeDamage(damage);
                }
                DestroyProjectile();
            }
        }

        transform.Translate(forward * speed * Time.deltaTime);
        //Debug.LogError(forward);
    }
    public void DestroyProjectile()
    {
        //GameObject deathEffect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // Destroy(deathEffect, 1.0f);
    }
}