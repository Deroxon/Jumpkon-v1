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
        AudioManager.Instance.ControlMusicVolume(musicSlider.value);
        AudioManager.Instance.ControlSFXVolume(sfxSlider.value);
        AudioManager.Instance.ControlMonsterSFXVolume(monsterSlider.value);
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
                currentImage = MusicIcon.GetComponent<Image>();
                break;

            case "SFXMonsters":
                currentAudioSource = AudioManager.Instance.sfxMonsterSource;
                currentImage = MusicIcon.GetComponent<Image>();
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
        switch (parent)
        {
            case "Music":
                AudioManager.Instance.ToggleMusic();
            break;

            case "SFX":
                AudioManager.Instance.ToggleSFX();
            break;

            case "SFXMonsters":
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
