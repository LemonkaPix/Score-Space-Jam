using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] PlayerController controller;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject abilityIcon;
    [SerializeField] GameObject experienceText;
    [SerializeField] GameObject pathSelector;

    // Update is called once per frame
    void Update()
    {
        experienceText.GetComponent<TextMeshProUGUI>().text = $"{controller.plrExperience} xp";
        experienceText.transform.Find("EvolveExp").GetComponent<TextMeshProUGUI>().text = $"{controller.EvolvesList[controller.CurrentEvolve]} xp to evolve";
        healthBar.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"{controller.plrHealth} hp";

        // REWORK THIS CODE LATER
        healthBar.transform.Find("HpFill").GetComponent<Image>().fillAmount = Mathf.Lerp(controller.plrHealth / 100, 1, Time.deltaTime);
    }
}
