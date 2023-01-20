using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] PlayerController playerController;
    private void OnEnable()
    {
        optionsPanel.SetActive(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        playerController.IsPaused = false;
    }

    public void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
