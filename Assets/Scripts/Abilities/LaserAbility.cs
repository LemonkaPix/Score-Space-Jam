using Managers.Sounds;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LaserAbility : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] Sprite abilitySprite;
    PlayerController playerController;
    public float abilityCooldown;
    bool isDelayed = true;
    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        playerController.changeUiImage(abilitySprite);
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
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.EnviromentSource, SoundManager.Instance.EnviromentCollection.clips[6]);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.layer = 9;
        playerController.uiCooldown(abilityCooldown);
        yield return new WaitForSeconds(abilityCooldown);
        isDelayed = true;
    }
}
