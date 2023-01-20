using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] AudioMixer[] audioMixers;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        foreach (var mixer in audioMixers)
        {
            mixer.SetFloat("Volume", volume);
        }
    }
}
