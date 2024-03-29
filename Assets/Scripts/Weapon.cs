using Managers.Sounds;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class Weapon : MonoBehaviour
{
    float fireRate;
    int damage;
    [SerializeField] Transform firePoint;
    SoundManager soundManager;
    public GameObject bulletPrefab;
    bool isDelayed = true;
    [SerializeField] Color bulletColor;
    PlayerController playerController;
    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        soundManager = SoundManager.Instance;
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
        soundManager.PlayOneShoot(soundManager.EnviromentSource, soundManager.EnviromentCollection.clips[2]);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletStats = bullet.GetComponent<Bullet>();
        bulletStats.damage = damage + (playerController.DamageLevel * playerController.DamageValue);
        bulletStats.speed =  playerController.plrBulletSpeed + (playerController.FireRateLevel * playerController.BulletSpeeedValue);
        bulletStats.canDamagePlayer = false;
        bullet.layer = 9;
        bullet.GetComponent<SpriteRenderer>().color = bulletColor;
        yield return new WaitForSeconds(60 / (playerController.plrFireRate + (playerController.FireRateLevel * playerController.FireRateValue)));
        isDelayed = true;
    }
}
