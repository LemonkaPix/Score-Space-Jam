using Managers.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject abilityPrefab;
    public Sprite abilitySprite;
    [SerializeField] Transform firePoint;
    PlayerController playerController;

    [Header("Stats")]
    public bool onCooldown;
    [SerializeField] int damage;
    [SerializeField] float abilityCooldown;

    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        playerController.changeUiImage(abilitySprite);
    }

    IEnumerator AbilityCooldown()
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.EnviromentSource, SoundManager.Instance.EnviromentCollection.clips[3]);
        GameObject bombClone = Instantiate(abilityPrefab, firePoint.position, firePoint.rotation);
        bombClone.name = "Bomb";
        onCooldown = true;

        playerController.uiCooldown(abilityCooldown);
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }

    void Update()
    {
            if (Input.GetKeyDown("space") && !onCooldown)
            {
                StartCoroutine(AbilityCooldown());
            }
    }
}
