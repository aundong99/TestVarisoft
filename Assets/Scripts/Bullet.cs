using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            MeleeEnemy enemyScript = collision.GetComponent<MeleeEnemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(10);
            }

            BatEnemy enemyyScript = collision.GetComponent<BatEnemy>();
            if (enemyyScript != null)
            {
                enemyyScript.TakeDamage(10);
            }
            Destroy(gameObject);
        }
    }
}
