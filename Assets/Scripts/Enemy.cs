using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public float fireRate;
    public int damage;
    [SerializeField] Transform firePoint;
    public GameObject bulletPrefab;
    bool isDelayed = true;


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
        if (isDelayed)
        {
            isDelayed = false;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        yield return new WaitForSeconds(2);
        isDelayed = true;
    }

}
