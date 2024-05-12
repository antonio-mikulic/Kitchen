﻿using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC = "Music";

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume = 0.5f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC, volume);
        audioSource.volume = volume;
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1f)
            volume = 0f;

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC, volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}