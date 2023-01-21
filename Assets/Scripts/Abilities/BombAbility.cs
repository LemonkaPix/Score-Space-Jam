using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject abilityPrefab;
    public Sprite abilitySprite;
    [SerializeField] Transform firePoint;

    [Header("Stats")]
    public bool onCooldown;
    [SerializeField] int damage;
    [SerializeField] int abilityCooldown;

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
