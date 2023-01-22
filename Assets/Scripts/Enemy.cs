using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    body,
    shooter,
    sniper,
    shotgunner,
    spammer
}

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] EnemyType enemyType;
    public int health = 100;

    public float fireRate;
    public int damage;
    [SerializeField] Transform[] firePoints;
    public GameObject bulletPrefab;
    bool isDelayed = true;
    [SerializeField] float attackRange;
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed;
    [SerializeField] Color bulletColor;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 offset = player.position - transform.position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);

        if (enemyType == EnemyType.body)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (FindObjectOfType<PlayerMovement>().isDashing) return;
            if (Vector2.Distance(player.position, transform.position) < attackRange)
            {
                player.GetComponent<PlayerController>().TakeDamage(damage);
                Die();
            }
        }
        else
        {
            if (Vector2.Distance(player.position, transform.position) >= attackRange)
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            else if (isDelayed)
            {
                isDelayed = false;
                StartCoroutine(Shoot());
            }

        }
    }

    private IEnumerator Shoot()
    {
        foreach (var firePoint in firePoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.GetComponent<Bullet>().speed = bulletSpeed;
            bullet.layer = 8;
            bullet.GetComponent<SpriteRenderer>().color = bulletColor;
        }
        yield return new WaitForSeconds(60 / fireRate);
        isDelayed = true;
    }

}
