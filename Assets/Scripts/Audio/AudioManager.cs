using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer audioMixer;
    private AudioSource audioSource;
    private bool isApplicationPaused;
    private int currentSong;
    private List<string> MusicStringList = new List<string>
    {
        "GameMenuTheme",
        "GameMenuTheme2",
        "GameMenuTheme3",
        "GameMenuTheme4",
    };

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
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
        LoadSettings();
        PlayMusic("GameMenuTheme");
        currentSong = 0;
    }

    private void FixedUpdate()
    {
        if (audioSource != null && !audioSource.isPlaying && !isApplicationPaused)
        {
                if (currentSong == 4)
                {
                    currentSong = 0;
                }
                currentSong++;
                PlayMusic(MusicStringList[currentSong - 1]);
        }
    }

    public Sound[] musicSounds, sfxSounds, sfxMonsters;
    public AudioSource musicSource, sfxSource, sfxMonsterSource;

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if(s == null)
        {
            Debug.LogError("Sound not found: " + name);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void StopMusic() => musicSource.Stop();
    // Little chaotic function, the most important is to return monsterSFX Sound to EnemyLogicScript to use it for separate audioSources
    public Sound PlaySFX(string name)
    {
        Sound playerSFX = Array.Find(sfxSounds, x => x.name == name);
        Sound monsterSFX = Array.Find(sfxMonsters, x => x.name == name);
        if (playerSFX == null)
        {
            if (monsterSFX == null)
            {
                return new Sound();
            }
            else
            {
                return monsterSFX;
            }
        }
        else
        {
            sfxSource.PlayOneShot(playerSFX.clip);
            return new Sound();
        }
    }

    public void StopAllAudio()
    {
        musicSource.Stop();
        sfxMonsterSource.Stop();
        sfxSource.Stop();
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
        float currentValueDb;
        bool audioMixerDbValue = audioMixer.GetFloat("VolumeMonsterSFX",  out currentValueDb);

        if(currentValueDb == -80)
        {
            audioMixer.SetFloat("VolumeMonsterSFX", PlayerPrefs.GetFloat("SavedDbMonsterValue",0f));
        } else
        {
            SaveAudioSettings("SavedDbMonsterValue", currentValueDb);
            audioMixer.SetFloat("VolumeMonsterSFX", -80);
        }
        sfxMonsterSource.mute = !sfxMonsterSource.mute;
    }

    public void ControlMusicVolume(float volume)
    {
        musicSource.volume = volume;
        SaveAudioSettings("Music", volume);
    }
    public void ControlSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveAudioSettings("SFXVolume", volume);
    }

    public void ControlMonsterSFXVolume(float volume)
    {
        // there is separate audioSource for each enemy, so we need to have an influence of the audioMixer and not the AudiosourceVolume 
        float volumeInDb = Mathf.Log10(volume) * 20;
        if(volume < 0.02) { volumeInDb = -80; } // save check to not have Infinity value in Db
        audioMixer.SetFloat("VolumeMonsterSFX", volumeInDb);
        SaveAudioSettings("SFXMonsterVolume", volume);
        SaveAudioSettings("SFXMonsterVolumeDb", volumeInDb);
    }


    public void SaveAudioSettings(string key, float value)
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainMenu.Instance.SaveSettings(new KeyValuePair<string, float>[] { new KeyValuePair<string, float>(key, value), });
        } else
        {
            PauseMenu.Instance.SaveSettings(new KeyValuePair<string, float>[] { new KeyValuePair<string, float>(key, value), });
        }
        
    }

    [ContextMenu("LoadSettings")]
    public void LoadSettings()
    {
        string[] namesSettingstoLoad = { "Music", "SFXVolume", "SFXMonsterVolumeDb" };
        // Initalize array with keyvaluePair
        var settingstoLoad = new KeyValuePair<string, float>[namesSettingstoLoad.Length];

        for (int i = 0; i< namesSettingstoLoad.Length; i++)
        {
            string settingName = namesSettingstoLoad[i];
            float settingValue = PlayerPrefs.GetFloat(settingName);
            settingstoLoad[i] = new KeyValuePair<string, float>(settingName, settingValue); 
        }
        // Loading Music, SFX, MonsterSFX
        musicSource.volume = settingstoLoad[0].Value;
        sfxSource.volume = settingstoLoad[1].Value;

        if (settingstoLoad[2].Key == "SFXMonsterVolumeDb")
        {
            if (settingstoLoad[2].Value < 0.02) { audioMixer.SetFloat("VolumeMonsterSFX", -80); } // save check to not have Infinity value in Db

            audioMixer.SetFloat("VolumeMonsterSFX", settingstoLoad[2].Value);
        }
       
    }

    private void OnApplicationFocus(bool focus)
    {
        isApplicationPaused = !focus;
    }


}
