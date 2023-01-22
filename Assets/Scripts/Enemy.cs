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
    PlayerController playerController;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] EnemyType enemyType;
    public int health = 100;

    public float fireRate;
    public int damage;
    [SerializeField] Transform[] firePoints;
    public GameObject bulletPrefab;
    bool isDelayed = true;
    [SerializeField] int experience;
    [SerializeField] float attackRange;
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed;
    [SerializeField] Color bulletColor;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        playerController = FindObjectOfType<PlayerController>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamageVisual());
        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator DamageVisual()
    {
        Color basecolor = spriteRenderer.color;
        spriteRenderer.color = UnityEngine.Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = basecolor;
    }

    private void Die()
    {
        Destroy(gameObject);
        playerController.AddExperience(experience);
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
