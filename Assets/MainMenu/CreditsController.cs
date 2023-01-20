using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    public void HideCredits()
    {
        optionsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
