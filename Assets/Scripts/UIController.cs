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
    [SerializeField] GameObject experienceText;
    [SerializeField] GameObject pathSelector;

    [SerializeField] Image[] fireRateUpBar;
    [SerializeField] Image[] speedUpBar;
    [SerializeField] Image[] damageUpBar;
    [SerializeField] Image[] healthUpBar;


    // Update is called once per frame
    void Update()
    {
        experienceText.GetComponent<TextMeshProUGUI>().text = $"{controller.plrExperience} xp";
        experienceText.transform.Find("EvolveExp").GetComponent<TextMeshProUGUI>().text = $"{controller.EvolvesList[controller.CurrentEvolve]} xp to evolve";
        healthBarLerp.fillAmount = Mathf.Lerp(healthBarLerp.fillAmount, controller.plrHealth / 100, 3 * Time.deltaTime);

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
    public void UpdateHealthaBar()
    {
        healthBarFill.fillAmount = controller.plrHealth / 100;
        healthBarText.text = $"{controller.plrHealth} hp";
    }

}
