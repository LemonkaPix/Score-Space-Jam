using Managers.Sounds;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Security;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed;
    [SerializeField] float lifeTime;
    [SerializeField] Rigidbody2D rb;
    public int damage;
    public bool canDamagePlayer = true;

    [Header("Bomb Ability")]
    public bool isBomb;
    [SerializeField] GameObject BombExplosion;
    [SerializeField] TrailRenderer trail;
    [SerializeField] SpriteRenderer sprite;
    private void Start()
    {
        trail.startColor = sprite.color;
        trail.endColor = new Color32(0, 0, 0, 0);
        rb.velocity = transform.up * speed;
        StartCoroutine(LifeTime());
    }

    IEnumerator ExplosionDamage(Vector2 center, float radius)
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.EnviromentSource, SoundManager.Instance.EnviromentCollection.clips[3]);
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
        yield return new WaitForSeconds(0.5f);
        Destroy(bombExplosion);
        Destroy(gameObject);
    }


    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        if (!isBomb) Destroy(gameObject);
        else
        {
            rb.velocity = transform.up * 0;
            StartCoroutine(ExplosionDamage(transform.position, 10));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Weapon>() && !canDamagePlayer) return;
        if (collision.GetComponent<Bullet>()) return;
        if (collision.GetComponent<Enemy>() && canDamagePlayer) return;

        if (collision.transform.parent)
        {
            PlayerMovement dashCheck = collision.transform.parent.GetComponent<PlayerMovement>();
            if (dashCheck != null && dashCheck.isDashing) return;
        }


        if (isBomb)
        {
            StartCoroutine(ExplosionDamage(transform.position, 10));
        }
        else
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Weapon player = collision.GetComponent<Weapon>();
            if (enemy != null) enemy.TakeDamage(damage);
            else if (player != null) player.gameObject.transform.parent.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
