using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject abilityPrefab;
    [SerializeField] int abilityCooldown;
    [SerializeField] int abilityLifeTime;
    [SerializeField] int shieldAmount;
    public Sprite abilitySprite;

    [Header("Stats")]
    public bool onCooldown;
    public bool onUse;
    
    IEnumerator AbilityCooldown()
    {
        GameObject shieldClone = Instantiate(abilityPrefab, transform.position, transform.rotation, transform.parent);
        shieldClone.name = "Shield";
        onUse = true;
        onCooldown = true;

        yield return new WaitForSeconds(abilityLifeTime);
        onUse = false;
        Destroy(shieldClone);

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
