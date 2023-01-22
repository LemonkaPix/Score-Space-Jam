using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [HideInInspector] public bool canTakeDamage = true;
    [HideInInspector] public bool isDuringRam = false;

    [Header("Evolution")]
    public int currentPath = 0;   //0-circle, 1-traingle, 2-square, 3-diamond, 4-hexagon
    public int currentPathEvo = 0;
    public GameObject[] Shapes;
    [HideInInspector] public List<int> EvolutionCost = new List<int>() { 100, 200, 300, 400};

    [Header("Upgrades")]
    public int HealthLevel = 0;
    public int DamageLevel = 0;
    public int SpeedLevel = 0;
    public int FireRateLevel = 0;

    [Header("PlayerStats")]
    public float plrHealth = 100;
    public float plrSpeed = 5f;
    public int plrDamage = 10;
    public float plrFireRate = 60;
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
        if (plrExperience == EvolutionCost[currentPathEvo]) Evolution(currentPath);
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
        if (playerMovement.isDashing == false && !isDuringRam) { ramDamage = 0; return; }

        if (playerMovement.isDashing) ramDamage = playerMovement.dashDamage;

        enemy.TakeDamage(ramDamage);
        StartCoroutine(ramCooldown());

    }

    public void Evolution(int evolve)
    {

        plrExperience = 0;
        if(currentPath == 0)
        {
            // Circle evolution
            Debug.Log("evolve circle here");
            return;
        }
        currentPathEvo++;

        for(int i = 0; i < Shapes.Length; i++)
        {
            GameObject currentShape = Shapes[i];
            EvolutionData evoData = currentShape.GetComponent<EvolutionData>();
            if (evoData.pathId == currentPath && evoData.pathEvolution == currentPathEvo)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
                Instantiate(currentShape, gameObject.transform);
                plrHealth = evoData.Health;
                plrSpeed = evoData.Speed;
                plrDamage = evoData.Damage;
                plrFireRate = evoData.FireRate;
            }
        }
        
    }
    public void TakeDamage(int damage)
    {
        if(!canTakeDamage) return;
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

    public void AddExperience(int exp)
    {
        plrExperience += exp;
        uiController.experienceText.text = $"{plrExperience} xp";
    }
}
