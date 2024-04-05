using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [SerializeField] float intialSFXVolume = 0.8f;
    [SerializeField] float initialMusicVolume = 0.1f;

    public static SoundManager Instance
    {
        get
        {
            // Try to find instance in the scene if not found
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                // If it's still null, create a new instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SoundManager");
                    instance = singletonObject.AddComponent<SoundManager>();
                }
            }

            return instance;
        }
    }

    void Start()
    {
        // Ensure there's only one instance
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Initalize audio sources
        soundSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        // Set inital volumes
        soundSource.volume = intialSFXVolume;
        musicSource.volume = initialMusicVolume;

        // Start with music
        if (musicClips.Length > 0)
        {
            PlayMusic(0);
        }
    }

    public AudioClip[] soundClips;
    private AudioSource soundSource;

    public AudioClip[] musicClips;
    private AudioSource musicSource;

    public void PlaySound(int soundIndex)
    {
        // Play sound if in range
        if (soundIndex >= 0 && soundIndex < soundClips.Length)
        {
            soundSource.PlayOneShot(soundClips[soundIndex]);
        }
        else
        {
            Debug.LogWarning("Invalid sound index: " + soundIndex);
        }
    }

    public void PlayMusic(int musicIndex)
    {
        // Start music if in range
        if (musicIndex >= 0 && musicIndex < musicClips.Length)
        {
            musicSource.clip = musicClips[musicIndex];
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music index: " + musicIndex);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        soundSource.volume = volume;
    }

    public float GetSFXVolume()
    {
        return soundSource.volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }
}