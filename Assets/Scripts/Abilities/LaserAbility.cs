using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LaserAbility : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    public GameObject bulletPrefab;
    PlayerController playerController;
    public int abilityDelay;
    bool isDelayed = true;
    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDelayed)
        {
            isDelayed = false;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.layer = 9;
        yield return new WaitForSeconds(abilityDelay);
        isDelayed = true;
    }
}
