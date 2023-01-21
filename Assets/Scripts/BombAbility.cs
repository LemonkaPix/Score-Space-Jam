using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject abilityPrefab;
    [SerializeField] int abilityCooldown;
    [SerializeField] int damage;
    public Sprite abilitySprite;
    [SerializeField] Transform firePoint;

    [Header("Stats")]
    public bool onCooldown;


    IEnumerator AbilityCooldown()
    {
        GameObject bombClone = Instantiate(abilityPrefab, firePoint.position, firePoint.rotation);
        bombClone.name = "Bomb";
        onCooldown = true;

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
