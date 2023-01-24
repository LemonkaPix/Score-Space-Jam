using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] AudioMixer[] audioMixers;
    [SerializeField] TMP_Text percentage;
    [SerializeField] Slider slider;
    private void OnEnable()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        slider.value = volume;
        percentage.text = Mathf.Round((volume + 40) / 40 * 100).ToString();
        SetVolume(volume);
    }
    public void CloseOptionsPanel()
    {
        gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void ShowCreditsPanel()
    {
        gameObject.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        foreach (var mixer in audioMixers)
        {
            mixer.SetFloat("Volume", volume);
        }
        percentage.text = Mathf.Round((volume + 40) / 40 * 100).ToString();
        PlayerPrefs.SetFloat("Volume",volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
