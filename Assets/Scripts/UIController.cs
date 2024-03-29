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
    public TMP_Text evolveText;
    [SerializeField] GameObject pathSelector;
    [SerializeField] GameObject upgrades;


    [SerializeField] Image[] fireRateUpBar;
    [SerializeField] Image[] speedUpBar;
    [SerializeField] Image[] damageUpBar;
    [SerializeField] Image[] healthUpBar;


    // Update is called once per frame
    void Update()
    {
        healthBarLerp.fillAmount = Mathf.Lerp(healthBarLerp.fillAmount, controller.plrHealth / controller.maxPlrHealth, 3 * Time.deltaTime);
        EvolveXpText();
    }

    [Button]

    public void changeAbilityImage(Sprite sprite)
    {
        abilityIcon.GetComponent<Image>().sprite = sprite; 
    }

    public IEnumerator abilityCooldown(float cooldown)
    {
        float startTime = Time.time;
        Image cooldownOverlay = abilityIcon.transform.parent.Find("load").GetComponent<Image>();
        cooldownOverlay.gameObject.SetActive(true);
        cooldownOverlay.fillAmount = 0;

        while(cooldownOverlay.fillAmount != 1)
        {
            float timePassed = Time.time - startTime;
            cooldownOverlay.fillAmount = Mathf.Lerp(cooldownOverlay.fillAmount, timePassed / cooldown, 3 * Time.deltaTime);
            yield return new WaitForSeconds(0f);
        }
        cooldownOverlay.gameObject.SetActive(false);
    }

    public void choosePath(int index) // 1 - triangle, 2 - square, 3 - rhomb, 4 - hexagon
    {
        upgrades.SetActive(true);
        controller.currentPath = index;
        controller.spawnFigure();
        pathSelector.SetActive(false);
        controller.plrExperience -= controller.EvolutionCost[controller.currentPathEvo];
        UpdateXP();
        controller.currentPathEvo++;
    }

    public void EvolveXpText()
    {
        if (controller.currentPathEvo < 3)
        {
            evolveText.text = $"{controller.EvolutionCost[controller.currentPathEvo]} xp to evolve";
        }
        else
        {
            evolveText.text = "You can't evolve further";
        }
    }

    [Button]
    public void UpgradeBarFiller()
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
    public void UpdateXP()
    {
        experienceText.text = $"{controller.plrExperience} xp";
    }

}
