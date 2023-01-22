using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] PlayerController controller;
    [SerializeField] Image healthBarFill;
    [SerializeField] Image healthBarLerp;
    [SerializeField] TMP_Text healthBarText;
    [SerializeField] GameObject abilityIcon;
    public TMP_Text experienceText;
    [SerializeField] GameObject pathSelector;

    [SerializeField] Image[] fireRateUpBar;
    [SerializeField] Image[] speedUpBar;
    [SerializeField] Image[] damageUpBar;
    [SerializeField] Image[] healthUpBar;


    // Update is called once per frame
    void Update()
    {
        healthBarLerp.fillAmount = Mathf.Lerp(healthBarLerp.fillAmount, controller.plrHealth / 100, 3 * Time.deltaTime);
    }

    [Button]

    public void choosePath(int index) // 1 - triangle, 2 - square, 3 - rhomb, 4 - hexagon
    {
        controller.currentPath = index;
        controller.spawnFigure();
        pathSelector.SetActive(false);
    }

    public void EvolveXpText()
    {
        experienceText.text = $"{controller.EvolutionCost[controller.currentPathEvo]} xp to evolve";
    }

    [Button]
    void UpgradeBarFiller()
    {
        for (int i = 0; i < 5; i++)
        {
            fireRateUpBar[i].enabled = (i < controller.FireRateLevel);
            speedUpBar[i].enabled = (i < controller.SpeedLevel);
            damageUpBar[i].enabled = (i < controller.DamageLevel);
            healthUpBar[i].enabled = (i < controller.HealthLevel);
        }
    }
    [Button]
    public void UpdateHealthBar()
    {
        healthBarFill.fillAmount = controller.plrHealth / controller.maxPlrHealth;
        healthBarText.text = $"{controller.plrHealth} hp";
    }

}
