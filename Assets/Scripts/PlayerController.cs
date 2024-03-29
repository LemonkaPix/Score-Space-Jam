using Managers.Sounds;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
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

    [Header("Leaderboard")]
    [SerializeField] LeaderBoard leaderboard;

    //sounds


    [Header("UI")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsPanel;
    [HideInInspector] public bool IsPaused = false;
    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject PathSelection;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject hitParticle;
    [SerializeField] TMP_Text timeAlive;
    [SerializeField] TMP_Text totalScore;
    [SerializeField] GameObject ability;
    public bool isGameOver = false;

    bool onRamCooldown;
    [HideInInspector] public int ramDamage;
    [HideInInspector] public bool canTakeDamage = true;
    [HideInInspector] public bool isDuringRam = false;

    [Header("Evolution")]
    public int currentPath = 0;   //0-circle, 1-traingle, 2-square, 3-diamond, 4-hexagon
    public int currentPathEvo = 0;
    public GameObject[] Shapes;
    [HideInInspector] public List<int> EvolutionCost = new List<int>() { 100, 200};

    [Header("Upgrades")]
    public int UpgradePrize;
    [HideInInspector] public int HealthLevel = 0;
    [HideInInspector] public int DamageLevel = 0;
    [HideInInspector] public int SpeedLevel = 0;
    [HideInInspector] public int FireRateLevel = 0;

    public int HealthValue = 50;
    public int DamageValue = 50;
    public float SpeedValue = 50;
    public int FireRateValue = 50;
    public int BulletSpeeedValue = 2;

    [Header("PlayerStats")]
    public float maxPlrHealth = 100;
    public float plrHealth = 100;
    public float plrSpeed = 5f;
    public int plrDamage = 10;
    public float plrFireRate = 60;
    public float plrBulletSpeed = 8;
    public int plrExperience = 0;
    public int score = 0;
    Stopwatch stopwatch = new Stopwatch();


    void Start()
    {
        stopwatch.Start();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            IsPaused = !IsPaused;
            if (IsPaused)
            {
                stopwatch.Stop();
                //gameUi.SetActive(false);
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                stopwatch.Start();
                //gameUi.SetActive(true);
                pauseMenu.SetActive(false);
                optionsPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (plrExperience >= EvolutionCost[currentPathEvo] && currentPathEvo < 3) Evolution(currentPath);
    }

    public void changeUiImage(Sprite sprite) 
    {
        uiController.changeAbilityImage(sprite);
    }

    public void uiCooldown(float cooldown)
    {
        StartCoroutine(uiController.abilityCooldown(cooldown));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null) return;
        
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
        if (playerMovement.isDashing == false && !isDuringRam) { ramDamage = 0; return; }

        if (playerMovement.isDashing) ramDamage = playerMovement.dashDamage;

        enemy.TakeDamage(ramDamage);

    }


    public void spawnFigure()
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.EnviromentSource, SoundManager.Instance.EnviromentCollection.clips[7]);
        for (int i = 0; i < Shapes.Length; i++)
        {
            GameObject currentShape = Shapes[i];
            EvolutionData evoData = currentShape.GetComponent<EvolutionData>();
            if (evoData.pathId == currentPath && evoData.pathEvolution == currentPathEvo)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
                Instantiate(currentShape, gameObject.transform);
                maxPlrHealth = evoData.Health + (HealthLevel * HealthValue);
                plrHealth = evoData.Health + (HealthLevel * HealthValue);
                if (plrHealth > maxPlrHealth) plrHealth = maxPlrHealth;
                uiController.UpdateHealthBar();
                plrSpeed = evoData.Speed;
                plrDamage = evoData.Damage;
                plrFireRate = evoData.FireRate;
            }
        }
    }

    public void Evolution(int evolve)
    {
        switch (currentPathEvo)
        {
            case 0:
                // Circle evolution
                PathSelection.SetActive(true);
                return;
                break;
            case 1:
                ability.SetActive(true);
                break;
            default:
                break;
        }

        plrExperience -= EvolutionCost[currentPathEvo];
        uiController.UpdateXP();
        currentPathEvo++;
        spawnFigure();
    }
    public void TakeDamage(int damage)
    {
        if(!canTakeDamage) return;
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.PlayerSource, SoundManager.Instance.PlayerCollection.clips[1]);
        Instantiate(hitParticle, transform.position, transform.rotation);
        plrHealth -= damage;
        uiController.UpdateHealthBar();
        if (plrHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.PlayerSource, SoundManager.Instance.PlayerCollection.clips[0]);
        isGameOver = true;
        stopwatch.Stop();
        Time.timeScale = 0f;
        if (currentPath != 0) score += 3000;
        score += plrExperience * 5;
        score += currentPathEvo * 3000;
        score += (int)stopwatch.Elapsed.TotalSeconds * 5;
        
        deathScreen.SetActive(true);
        timeAlive.text = $"You were alive for: {(stopwatch.Elapsed.Minutes != 0 ? stopwatch.Elapsed.Minutes + " minutes and " : "")}{stopwatch.Elapsed.Seconds} seconds";
        totalScore.text = $"Your score is: {score}";
        yield return leaderboard.SubmitScoreRoutine(score);

    }

    public void AddExperience(int exp)
    {
        plrExperience += exp;
        uiController.experienceText.text = $"{plrExperience} xp";
    }

    public void BuyUpgrade(int upgradeIndex)
    {
        switch (upgradeIndex)
        {
            case 1: //fire rate
                if (FireRateLevel < 5 && plrExperience >= UpgradePrize)
                {
                    FireRateLevel++;
                    plrExperience -= UpgradePrize;
                    SoundManager.Instance.PlayOneShoot(SoundManager.Instance.UISource, SoundManager.Instance.UICollection.clips[1]);
                }
                break;
            case 2: //speed
                if (SpeedLevel < 5 && plrExperience >= UpgradePrize)
                {
                    SpeedLevel++;
                    plrExperience -= UpgradePrize;
                    SoundManager.Instance.PlayOneShoot(SoundManager.Instance.UISource, SoundManager.Instance.UICollection.clips[1]);

                }
                break;
            case 3: //damage
                if (DamageLevel < 5 && plrExperience >= UpgradePrize)
                {
                    DamageLevel++;
                    plrExperience -= UpgradePrize;
                    //plrDamage += DamageValue;
                    SoundManager.Instance.PlayOneShoot(SoundManager.Instance.UISource, SoundManager.Instance.UICollection.clips[1]);

                }
                break;
            case 4: //heath
                if (HealthLevel < 5 && plrExperience >= UpgradePrize)
                {
                    HealthLevel++;
                    plrExperience -= UpgradePrize;
                    plrHealth += HealthValue;
                    maxPlrHealth += HealthValue;
                    uiController.UpdateHealthBar();
                    SoundManager.Instance.PlayOneShoot(SoundManager.Instance.UISource, SoundManager.Instance.UICollection.clips[1]);

                }
                break;
            default:
                break;
        }
        uiController.UpgradeBarFiller();
        uiController.UpdateXP();
    }
}
