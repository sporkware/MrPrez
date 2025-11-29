using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Audio clips (assign in inspector)
    public AudioClip[] musicTracks;
    public AudioClip gunshotClip;
    public AudioClip carEngineClip;
    public AudioClip footstepsClip;

    void Awake()
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

    void Start()
    {
        PlayMusic(0); // Play first track
        InvokeRepeating("UpdateMusic", 10f, 10f); // Check every 10 seconds
    }

    public void PlayMusic(int trackIndex)
    {
        if (musicTracks.Length > trackIndex && musicSource != null)
        {
            musicSource.clip = musicTracks[trackIndex];
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayGunshot()
    {
        PlaySFX(gunshotClip);
    }

    public void PlayCarEngine()
    {
        PlaySFX(carEngineClip);
    }

    public void PlayFootsteps()
    {
        PlaySFX(footstepsClip);
    }

    private void UpdateMusic()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            if (player.corruptionLevel > 50f && musicTracks.Length > 1)
            {
                PlayMusic(1); // Tense track
            }
            else
            {
                PlayMusic(0); // Patriotic track
            }
        }
    }
}