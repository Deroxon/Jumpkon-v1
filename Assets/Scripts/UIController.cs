using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public GameObject musicSettings;
    public GameObject settingsUiButton;
    public Boolean isMusicSettingsOn = false;

    private void Start()
    {
        AudioManager.Instance.ControlMusicVolume(musicSlider.value);
        AudioManager.Instance.ControlSFXVolume(sfxSlider.value);
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.ControlMusicVolume(musicSlider.value);
    }

    public void SfxVolume()
    {
        AudioManager.Instance.ControlSFXVolume(sfxSlider.value);
    }

    public void ToggleAudioController()
    {
        isMusicSettingsOn = !isMusicSettingsOn;
        musicSettings.SetActive(isMusicSettingsOn);
        settingsUiButton.SetActive(!isMusicSettingsOn);
    }


}
