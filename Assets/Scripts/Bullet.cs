using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    [SerializeField] float lifeTime;
    [SerializeField] Rigidbody2D rb;
    public int damage;

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
        Enemy enemy = collision.GetComponent<Enemy>();
        PlayerController player = collision.gameObject.transform.parent.GetComponent<PlayerController>();
        if (enemy != null) enemy.TakeDamage(damage);
        else if (player != null) player.TakeDamage(damage);
        Destroy(gameObject);
    }
}
