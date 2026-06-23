using System;
using System.Data.Common;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettingsController : MonoBehaviour
{
    [SerializeField] private Slider baseVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider ambienceVolumeSlider;

    [SerializeField] private AudioMixer audioMixer;

    private float volume;

    public void SetBaseVolume()
    {
        volume = baseVolumeSlider.value;

        SetVolume(baseVolumeSlider, "MasterVolume", volume);
        GameManager.Manager.BaseVolume = volume;
    }

    public void SetMusicVolume()
    {
        volume = musicVolumeSlider.value;

        SetVolume(musicVolumeSlider, "MusicVolume", volume);
        GameManager.Manager.MusicVolume = volume;
    }

    public void SetSFXVolume()
    {
        volume = sfxVolumeSlider.value;

        SetVolume(sfxVolumeSlider, "SFXVolume", volume);
        GameManager.Manager.SFXVolume = volume;
    }

    public void SetAmbienceVolume()
    {
        volume = ambienceVolumeSlider.value;

        SetVolume(ambienceVolumeSlider, "AmbienceVolume", volume);
        GameManager.Manager.AmbienceVolume = volume;
    }

    private void SetVolume(Slider slider, string name, float value)
    {  
        float db;

        TextMeshProUGUI percentage = slider.transform.GetComponentInChildren<TextMeshProUGUI>();

        if (value <= .0001f)
        {
            db = -80f;
        }
        else
        {
            db = MathF.Log10(value) * 20;
        }

        percentage.text = Mathf.FloorToInt(value * 100).ToString() + "%";
        audioMixer.SetFloat(name, db);
    }

    private void Start()
    {
        baseVolumeSlider.value = GameManager.Manager.BaseVolume;
        musicVolumeSlider.value = GameManager.Manager.MusicVolume;
        sfxVolumeSlider.value = GameManager.Manager.SFXVolume;
        ambienceVolumeSlider.value = GameManager.Manager.AmbienceVolume;
    }
}
