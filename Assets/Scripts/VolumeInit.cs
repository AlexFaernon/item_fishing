using System;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    [SerializeField] private string volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        var volume = PlayerPrefs.GetFloat(volumeParameter, 0f);
        audioMixer.SetFloat(volumeParameter, volume);
    }
}
