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
    public int activeShield;
    bool onUse;
    
    IEnumerator AbilityCooldown()
    {
        GameObject shieldClone = Instantiate(abilityPrefab, transform.position, transform.rotation, transform.parent);
        shieldClone.name = "Shield";

        activeShield = shieldAmount;
        onUse = true;
        onCooldown = true;

        yield return new WaitForSeconds(abilityLifeTime);
        onUse = false;
        Destroy(shieldClone);
        activeShield = 0;

        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && !onCooldown)
        {
            StartCoroutine(AbilityCooldown());
        }

        if(activeShield == 0 && onUse)
        {
            if(transform.Find("Shield"))
            {
                GameObject shield = transform.Find("Shield").gameObject;
                Destroy(shield);
            }
            
        }
    }
}
