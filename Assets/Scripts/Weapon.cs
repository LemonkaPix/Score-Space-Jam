using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(60 / fireRate);
        isDelayed = true;
    }
}
