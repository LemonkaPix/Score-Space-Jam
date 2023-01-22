using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject abilityPrefab;
    [SerializeField] int abilityCooldown;
    [SerializeField] int abilityLifeTime;
    public Sprite abilitySprite;

    [Header("Stats")]
    public bool onCooldown;
    public bool onUse;
    [SerializeField] int ramDamage;
    [SerializeField] float abilitySpeedBoost;
    
    IEnumerator AbilityCooldown()
    {        
        GameObject shieldClone = Instantiate(abilityPrefab, transform.position, transform.rotation, transform.parent);
        shieldClone.name = "Shield";
        Weapon weapon = transform.GetComponent<Weapon>();
        PlayerController pc = transform.parent.GetComponent<PlayerController>();
        float oldSpeed = pc.plrSpeed;

        onUse = true;
        onCooldown = true;
        weapon.enabled = false;
        pc.ramDamage = ramDamage;
        pc.canTakeDamage = false;
        pc.isDuringRam = true;
        pc.plrSpeed = abilitySpeedBoost;

        yield return new WaitForSeconds(abilityLifeTime);

        onUse = false;
        Destroy(shieldClone);
        weapon.enabled = true;
        pc.canTakeDamage = true;
        pc.isDuringRam = false;
        pc.plrSpeed = oldSpeed;

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
