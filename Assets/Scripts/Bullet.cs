using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] Rigidbody2D rb;
    public int damage;

    [Header("Bomb Ability")]
    public bool isBomb;
    [SerializeField] GameObject BombExplosion;

    private void Start()
    {
        rb.velocity = transform.up * speed;
        StartCoroutine(LifeTime());
    }

    IEnumerator ExplosionDamage(Vector2 center, float radius)
    {
        Debug.Log("h");
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius);
        GameObject bombExplosion = Instantiate(BombExplosion, transform.position, transform.rotation, transform.parent);
        transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            foreach (var Enemies in hits)
            {
                if (Enemies.GetComponent<Enemy>())
                {
                    Debug.Log("Enemy take dmg");
                    Enemies.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        yield return new WaitForSeconds(1f);
        Destroy(bombExplosion);
        Destroy(gameObject);
    }


    IEnumerator LifeTime()
    {
        if(isBomb)
        {
            yield return new WaitForSeconds(0.1f);
            CircleCollider2D collider = transform.GetComponent<CircleCollider2D>();
            collider.enabled = true;
        }
        
        
        yield return new WaitForSeconds(lifeTime);
        if(!isBomb) Destroy(gameObject);
        else
        {
            rb.velocity = transform.up * 0;
            ExplosionDamage(transform.position, 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (isBomb)
            {
                StartCoroutine(ExplosionDamage(transform.position, 10));
            }
            else
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
            }
    }
}
