using Managers.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [Header("Control variables")]
    public GameObject effectPrefab;
    public Sprite abilitySprite;
    PlayerController playerController;

    [Header("Stats")]
    public bool onCooldown;
    [SerializeField] float abilityCooldown;
    [SerializeField] float abilityLifeTime;
    [SerializeField] int damage;
    [SerializeField] float dashSpeed;

    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        playerController.changeUiImage(abilitySprite);
    }

    IEnumerator AbilityCooldown()
    {
        onCooldown = true;
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.EnviromentSource, SoundManager.Instance.EnviromentCollection.clips[4]);

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

        playerController.uiCooldown(abilityCooldown);
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }


    void Update()
    {
        //bool playerIsMoving = true;

        //if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) playerIsMoving = false;

        if (Input.GetKeyDown("space") && !onCooldown)
        {
            StartCoroutine(AbilityCooldown());
        }
    }
}
