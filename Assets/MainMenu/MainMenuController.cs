using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
