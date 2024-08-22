using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("GameMenuTheme");
        LoadSettings();
    }

    public Sound[] musicSounds, sfxSounds, sfxMonsters;
    public AudioSource musicSource, sfxSource, sfxMonsterSource;

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if(s == null)
        {
            Debug.LogError("Sound Not Found: " + name);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    // Little chaotic function, the most important is to return monsterSFX Sound to EnemyLogicScript to use it for separate audioSources
    public Sound PlaySFX(string name)
    {
        Sound playerSFX = Array.Find(sfxSounds, x => x.name == name);
        Sound monsterSFX = Array.Find(sfxMonsters, x => x.name == name);
        if (playerSFX == null)
        {
            if (monsterSFX == null)
            {

                Debug.LogError("Monster SFX Not Found: " + name);
                return new Sound();
            }
            else
            {
                Debug.Log("SFX Not Found: " + name + " but found monster SFX");
                return monsterSFX;
            }
        }
        else
        {
            sfxSource.PlayOneShot(playerSFX.clip);
            return new Sound();
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void ToggleMonsterSFX()
    {
        sfxMonsterSource.mute = !sfxMonsterSource.mute;
    }

    public void ControlMusicVolume(float volume)
    {
        musicSource.volume = volume;
        SaveSettings("Music", volume);
    }
    public void ControlSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveSettings("SFXVolume", volume);
    }

    public void ControlMonsterSFXVolume(float volume)
    {
        sfxMonsterSource.volume = volume;
        SaveSettings("SFXMonsterVolume", volume);
    }


    public void SaveSettings(string key, float value)
    {
        // Saving the value in the PlayerPrefs
        MainMenu.Instance.SaveSettings(new KeyValuePair<string, float>[] { new KeyValuePair<string, float>(key, value), });
    }

    [ContextMenu("LoadSettings")]
    public void LoadSettings()
    {
        string[] namesSettingstoLoad = { "Music", "SFXVolume", "SFXMonsterVolume" };
        // Initalize array with keyvaluePair
        var settingstoLoad = new KeyValuePair<string, float>[namesSettingstoLoad.Length];

        for (int i = 0; i< namesSettingstoLoad.Length; i++)
        {
            string settingName = namesSettingstoLoad[i];
            float settingValue = PlayerPrefs.GetFloat(settingName);
            settingstoLoad[i] = new KeyValuePair<string, float>(settingName, settingValue);
        }
        // Loading Music, SFX, MonsterSFX
        ControlMusicVolume(settingstoLoad[0].Value);
        ControlSFXVolume(settingstoLoad[1].Value);
        ControlMonsterSFXVolume(settingstoLoad[2].Value);

    }

}
