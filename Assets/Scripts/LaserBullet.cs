using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    public float speed;
    [SerializeField] float lifeTime;
    [SerializeField] Rigidbody2D rb;
    public int damage;
    public bool canDamagePlayer = true;

    private void Start()
    {
        rb.velocity = transform.up * speed;
        StartCoroutine(LifeTime());
    }



    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Weapon>() && !canDamagePlayer) return;
        if (collision.GetComponent<Bullet>()) return;
        if (collision.GetComponent<Enemy>() && canDamagePlayer) return;


        Enemy enemy = collision.GetComponent<Enemy>();
        Weapon player = collision.GetComponent<Weapon>();
        if (enemy != null) enemy.TakeDamage(damage);
        else if (player != null) player.gameObject.transform.parent.GetComponent<PlayerController>().TakeDamage(damage);
    }
}
