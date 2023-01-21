using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate;
    public int damage;
    [SerializeField] Transform firePoint;
    public GameObject bulletPrefab;
    EvolutionData EvolutionData = new();
    bool isDelayed = true;

    void Update()
    {
        if (Input.GetButton("Fire1") && isDelayed)
        {
            isDelayed = false;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        yield return new WaitForSeconds(60 / fireRate);
        isDelayed = true;
    }
}
