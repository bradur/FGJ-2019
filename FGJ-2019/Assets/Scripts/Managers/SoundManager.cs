
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{

    public static SoundManager main;

    [SerializeField]
    private SoundConfig config;

    private bool sfxMuted = false;

    [SerializeField]
    private bool musicMuted = false;
    public bool MusicMuted { get { return musicMuted; } }

    [SerializeField]
    private AudioSource sfxPlayer;

    [SerializeField]
    private AudioSource musicSource;

    private float originalMusicVolume = 0.5f;

    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        if (musicSource != null)
        {
            if (musicMuted)
            {
                musicSource.Pause();
                //UIManager.main.ToggleMusic();
            }
            else
            {
                musicSource.Play();
            }
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }

        audioSource.Pause();
        audioSource.volume = startVolume;
    }

    IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float targetVolume)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }
        
        audioSource.volume = targetVolume;
    }

    public void PauseMusic() {
        if (musicSource.isPlaying) {
            StartCoroutine(FadeOut(musicSource, 0.5f));
        }
    }

    public void UnpauseMusic() {
        if (!musicSource.isPlaying) {
            musicSource.UnPause();
            StartCoroutine(FadeIn(musicSource, 0.5f, originalMusicVolume));
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in config.Sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    sfxPlayer.clip = gameSound.sound;
                    if (gameSound.sounds.Count > 0)
                    {
                        sfxPlayer.clip = gameSound.sounds[Random.Range(0, gameSound.sounds.Count)];
                    }
                    sfxPlayer.Play();
                }
            }
        }
    }

    public void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
        //UIManager.main.ToggleSfx();
    }

    public bool ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
        //UIManager.main.ToggleMusic();
        return musicMuted;
    }
}
