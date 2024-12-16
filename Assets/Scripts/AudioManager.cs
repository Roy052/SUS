using UnityEngine;
public enum SFXType
{
    None = -1,
}

public class AudioManager : Singleton
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip[] clips;

    private void Awake()
    {
        audioManager = this;
    }

    public void PlaySfxAudio(SFXType type)
    {
        sfxSource.clip = clips[(int)type];
        sfxSource.Play();
    }
}
