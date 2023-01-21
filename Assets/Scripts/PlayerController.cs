using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

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
    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsPanel;
    [HideInInspector] public bool IsPaused = false;

    [Header("Evolution")]
    public int CurrentEvolve = 0;   //0-circle, 1-traingle, 2-square, 3-diamond, 4-hexagon
    public GameObject[] Evolves;

    [Header("Upgrades")]
    public int DamageLevel = 0;
    public int HealthLevel = 0;
    public int SpeedLevel = 0;
    public int FireRateLevel = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            if (IsPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                optionsPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }

    [Button]
    public void Evolution(int evolve)
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Instantiate(Evolves[evolve], gameObject.transform);
        CurrentEvolve = evolve;
    }
}
