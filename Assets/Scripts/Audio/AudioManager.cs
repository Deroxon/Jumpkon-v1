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
    public void PlaySFX(string name)
    {
        Sound playerSFX = Array.Find(sfxSounds, x => x.name == name);
        Sound monsterSFX = Array.Find(sfxMonsters, x => x.name == name);
        if (playerSFX == null)
        {
            if (monsterSFX == null)
            {

                Debug.LogError("Monster SFX Not Found: " + name);
            }
            else
            {
                Debug.LogError("SFX Not Found: " + name + " but found monster SFX");
                sfxSource.PlayOneShot(monsterSFX.clip);
            }
        }
        else
        {
            sfxSource.PlayOneShot(playerSFX.clip);
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
    }
    public void ControlSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void ControlMonsterSFXVolume(float volume)
    {
        sfxMonsterSource.volume = volume;
    }


}
