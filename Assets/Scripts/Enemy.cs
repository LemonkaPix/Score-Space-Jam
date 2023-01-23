using Managers.Sounds;
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
    public float health = 100;

    SoundManager soundManager = SoundManager.Instance;

    public float fireRate;
    public float damage;
    [SerializeField] Transform[] firePoints;
    [SerializeField] GameObject[] hitParticles;
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
        soundManager.PlayOneShoot(soundManager.EnviromentSource, soundManager.EnviromentCollection.clips[0]);
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
        switch (enemyType)
        {
            case EnemyType.body:
                Instantiate(hitParticles[0], transform.position, transform.rotation);
                break;
            case EnemyType.shooter:
                Instantiate(hitParticles[1], transform.position, transform.rotation);
                break;
            case EnemyType.shotgunner:
                Instantiate(hitParticles[2], transform.position, transform.rotation);
                break;
            case EnemyType.sniper:
                Instantiate(hitParticles[3], transform.position, transform.rotation);
                break;
            case EnemyType.spammer:
                Instantiate(hitParticles[4], transform.position, transform.rotation);
                break;

        }
        soundManager.PlayOneShoot(soundManager.EnviromentSource, soundManager.EnviromentCollection.clips[1]);
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
                player.GetComponent<PlayerController>().TakeDamage((int) damage);
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
        soundManager.PlayOneShoot(soundManager.EnviromentSource, soundManager.EnviromentCollection.clips[2]);
        foreach (var firePoint in firePoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().damage = (int) damage;
            bullet.GetComponent<Bullet>().speed = bulletSpeed;
            bullet.layer = 8;
            bullet.GetComponent<SpriteRenderer>().color = bulletColor;
        }
        yield return new WaitForSeconds(60 / fireRate);
        isDelayed = true;
    }

}
