using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsController : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] AudioMixer[] audioMixers;
    [SerializeField] TMP_Text percentage;
    public void CloseOptionsPanel()
    {
        gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        foreach (var mixer in audioMixers)
        {
            mixer.SetFloat("Volume", volume);
        }
        percentage.text = Mathf.Round((volume + 80) / 80 * 100).ToString();
    }
}
