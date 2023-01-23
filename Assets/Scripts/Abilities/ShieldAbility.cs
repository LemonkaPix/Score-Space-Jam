using Managers.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject abilityPrefab;
    [SerializeField] float abilityCooldown;
    [SerializeField] int abilityLifeTime;
    public Sprite abilitySprite;
    PlayerController playerController;
    [Header("Stats")]
    public bool onCooldown;
    public bool onUse;
    [SerializeField] int ramDamage;
    [SerializeField] float abilitySpeedBoost;
    
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        playerController.changeUiImage(abilitySprite);
    }

    IEnumerator AbilityCooldown()
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.EnviromentSource, SoundManager.Instance.EnviromentCollection.clips[5]);
        GameObject shieldClone = Instantiate(abilityPrefab, transform.position, transform.rotation, transform.parent);
        shieldClone.name = "Shield";
        Weapon weapon = transform.GetComponent<Weapon>();
        float oldSpeed = playerController.plrSpeed;

        onUse = true;
        onCooldown = true;
        weapon.enabled = false;
        playerController.ramDamage = ramDamage;
        playerController.canTakeDamage = false;
        playerController.isDuringRam = true;
        playerController.plrSpeed = abilitySpeedBoost;

        yield return new WaitForSeconds(abilityLifeTime);

        onUse = false;
        Destroy(shieldClone);
        weapon.enabled = true;
        playerController.canTakeDamage = true;
        playerController.isDuringRam = false;
        playerController.plrSpeed = oldSpeed;

        playerController.uiCooldown(abilityCooldown);
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && !onCooldown)
        {
            StartCoroutine(AbilityCooldown());
        }
    }
}
