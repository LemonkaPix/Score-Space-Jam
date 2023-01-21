using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject effectPrefab;
    public Sprite abilitySprite;

    [Header("Stats")]
    public bool onCooldown;
    [SerializeField] int abilityCooldown;
    [SerializeField] float abilityLifeTime;
    [SerializeField] int damage;
    [SerializeField] float dashSpeed;

    IEnumerator AbilityCooldown()
    {
        onCooldown = true;

        PlayerMovement plrMovement = transform.parent.GetComponent<PlayerMovement>();

        plrMovement.isDashing = true;
        plrMovement.dashSpeed = dashSpeed;
        plrMovement.dashDamage = damage;
        GameObject trail = Instantiate(effectPrefab, transform.position, transform.rotation, transform.parent);


        yield return new WaitForSeconds(abilityLifeTime);

        plrMovement.isDashing = false;
        plrMovement.dashSpeed = 0;
        plrMovement.dashDamage = 0;
        Destroy(trail);

        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }


    void Update()
    {
        bool playerIsMoving = true;

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) playerIsMoving = false;

        if (Input.GetKeyDown("space") && !onCooldown && playerIsMoving)
        {
            StartCoroutine(AbilityCooldown());
        }
    }
}
