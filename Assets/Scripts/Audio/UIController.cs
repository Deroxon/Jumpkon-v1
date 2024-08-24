using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider, monsterSlider;
    public GameObject musicSettings;
    public Boolean isMusicSettingsOn = false;
    public Sprite[] MusicImages;
    public GameObject MusicIcon, SfxIcon, MonsterSfxIcon;

    private void Start()
    {
        // read setted value
        AudioManager.Instance.LoadSettings();
        // Load values of sliders
        musicSlider.value = PlayerPrefs.GetFloat("Music", 0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);
        monsterSlider.value = PlayerPrefs.GetFloat("SFXMonsterVolume", 0f);
    }


    // jedna funkcja dla wszystkich slideów
    public void IdentifySlides(Transform parent)
    {
        Transform iconImage = parent.Find("Icon");
        AudioSource currentAudioSource = null;
        Image currentImage = null;

        // assigning values
        switch (parent.name)
        {
            case "Music":
                currentAudioSource = AudioManager.Instance.musicSource;
                currentImage = MusicIcon.GetComponent<Image>();
                break;

            case "SFX":
                currentAudioSource = AudioManager.Instance.sfxSource;
                currentImage = SfxIcon.GetComponent<Image>();
                break;

            case "MonsterSFX":
                currentAudioSource = AudioManager.Instance.sfxMonsterSource;
                currentImage = MonsterSfxIcon.GetComponent<Image>();
                break;
        }
        ToggleIcons(currentAudioSource, currentImage);
        ToggleSlides(parent.name);
    }

    private void ToggleIcons(AudioSource currentAudioSource, Image currentImage)
    {
        if (currentAudioSource != null)
        {
            if (currentImage != null)
            {
                if (currentAudioSource.mute)
                {
                    currentImage.sprite = MusicImages[0];
                }
                else
                {
                    currentImage.sprite = MusicImages[1];
                }
            }
            else
            {
                Debug.LogError("Variable currentImage has been not written");
            }
        }
        else
        {
            Debug.LogError("Variable currentAudioSource has been not written");
        }
    }

    private void ToggleSlides(string parent)
    {
        Debug.Log(parent);
        switch (parent)
        {
            case "Music":
                AudioManager.Instance.ToggleMusic();
            break;

            case "SFX":
                AudioManager.Instance.ToggleSFX();
            break;

            case "MonsterSFX":
                AudioManager.Instance.ToggleMonsterSFX();
            break;
        }
    }


    public void MusicVolume()
    {
        AudioManager.Instance.ControlMusicVolume(musicSlider.value);
    }

    public void SfxVolume()
    {
        AudioManager.Instance.ControlSFXVolume(sfxSlider.value);
    }

    public void SfxMonsterVolume()
    {
        AudioManager.Instance.ControlMonsterSFXVolume(monsterSlider.value);
    }

    public void ToggleAudioController()
    {
        isMusicSettingsOn = !isMusicSettingsOn;
        musicSettings.SetActive(isMusicSettingsOn);
    }


}
