using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] AudioMixer[] audioMixers;
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
    }
}
