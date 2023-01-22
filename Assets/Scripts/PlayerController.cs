using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public enum Evolve
{
    circle,
    traingle,
    square,
    diamond,
    hexagon
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] UIController uiController;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsPanel;
    [HideInInspector] public bool IsPaused = false;
    [SerializeField] GameObject gameUi;

    bool onRamCooldown;
    [HideInInspector] public int ramDamage;

    [Header("Evolution")]
    public int CurrentEvolve = 0;   //0-circle, 1-traingle, 2-square, 3-diamond, 4-hexagon
    public GameObject[] Evolves;
    [HideInInspector] public List<int> EvolvesList = new List<int>() { 100, 200, 300, 400, 500 };

    [Header("Upgrades")]
    public int HealthLevel = 0;
    public int DamageLevel = 0;
    public int SpeedLevel = 0;
    public int FireRateLevel = 0;

    [Header("PlayerStats")]
    public float plrHealth = 100;
    public float plrSpeed = 5f;
    public int plrDamage = 10;
    public int plrFireRate = 60;
    public int plrExperience = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            if (IsPaused)
            {
                gameUi.SetActive(false);
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                gameUi.SetActive(true);
                pauseMenu.SetActive(false);
                optionsPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator ramCooldown()
    {
        onRamCooldown = true;
        yield return new WaitForSeconds(0.5f);
        onRamCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null || onRamCooldown) return;
        
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
        if (playerMovement.isDashing == false) return;

        enemy.TakeDamage(playerMovement.dashDamage);
        StartCoroutine(ramCooldown());
    }

    public void Evolution(int evolve)
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Instantiate(Evolves[evolve], gameObject.transform);
        CurrentEvolve = evolve;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("taking damage");
        plrHealth -= damage;
        uiController.UpdateHealthaBar();
        if (plrHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("you died mother fucker");
    }
}
