using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 2f;
    public int direction = 1;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        rb.velocity = Vector3.right * direction * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            HpManager enemyHp = collision.gameObject.GetComponent<HpManager>();
            enemyHp.TakeDamage(damage);
            Destroy(gameObject, 0);
        }
    }
}
 