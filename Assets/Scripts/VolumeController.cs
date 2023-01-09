using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private string volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    private float volume;
    private const float Multiplier = 20f;
    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void Start()
    {
        volume = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) * Multiplier);
        slider.value = Mathf.Pow(10f, volume / Multiplier);
    }

    private void HandleSliderValueChanged(float value)
    {
        volume = Mathf.Log10(value) * Multiplier;
        audioMixer.SetFloat(volumeParameter, volume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, volume);
    }
}
