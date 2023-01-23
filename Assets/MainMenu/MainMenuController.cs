using Managers.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject leaderboard;
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
    public void ShowLeaderboard()
    {
        leaderboard.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CancelSound()
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.UISource, SoundManager.Instance.UICollection.clips[0]);
    }
    public void ConfirmSound()
    {
        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.UISource, SoundManager.Instance.UICollection.clips[1]);
    }
}
