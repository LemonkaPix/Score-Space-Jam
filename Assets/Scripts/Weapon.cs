using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    float fireRate;
    int damage;
    [SerializeField] Transform firePoint;
    public GameObject bulletPrefab;
    bool isDelayed = true;
    [SerializeField] Color bulletColor;
    PlayerController playerController;
    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    void Update()
    {
        fireRate = playerController.plrFireRate;
        damage = playerController.plrDamage;

        if (Input.GetButton("Fire1") && isDelayed)
        {
            isDelayed = false;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletStats = bullet.GetComponent<Bullet>();
        bulletStats.damage = damage + (playerController.DamageLevel * playerController.DamageValue);
        bulletStats.canDamagePlayer = false;
        bullet.layer = 9;
        bullet.GetComponent<SpriteRenderer>().color = bulletColor;
        yield return new WaitForSeconds(60 / (playerController.plrFireRate + (playerController.FireRateLevel * playerController.FireRateValue)));
        isDelayed = true;
    }
}
