using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour, IBootstrapper
{
    [Header("Audio sources")]
    [SerializeField]
    private AudioSource SoundAudioSource;
    [SerializeField]
    private AudioSource MusicAudioSource;

    [Header("Sounds and music")]
    [SerializeField]
    private AudioClip clickSound;
    [SerializeField]
    private AudioClip menuMusic;

    public void Init()
    {
       
        if (PlayerPrefs.GetString("isFirst") != "1")
        {
            PlayerPrefs.SetFloat("sound", 1);
            PlayerPrefs.SetFloat("music", 1);
            PlayerPrefs.SetString("isFirst", "1");
        }

        SetMusicVolume();
        SetSoundVolume();


        MusicAudioSource.loop = true;
        MusicAudioSource.clip = menuMusic;
        MusicAudioSource.Play();
    }

    public void PlayClickSound()
    {
        SoundAudioSource.PlayOneShot(clickSound);
    }


    public void ChangeSoundLevel(float value)
    {
        PlayerPrefs.SetFloat("sound", value);
        SetSoundVolume();
    }
    public void ChangeMusicLevel(float value)
    {
        PlayerPrefs.SetFloat("music", value);
        SetMusicVolume();
    }

    private void SetMusicVolume()
    {
        MusicAudioSource.volume = PlayerPrefs.GetFloat("music");
    }

    private void SetSoundVolume()
    {
        SoundAudioSource.volume = PlayerPrefs.GetFloat("sound");
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("music");
    }

    public float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("sound");
    }
}
