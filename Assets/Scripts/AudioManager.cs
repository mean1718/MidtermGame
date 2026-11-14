using UnityEngine;

public enum Sfx { Coin, Jump, Bounce, Fall, Win, GameOver, Checkpoint, Hit }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip music;
    [Range(0f, 1f)] public float musicVolume = 0.35f;

    [Header("Sound Effects")]
    public AudioClip coin;
    public AudioClip jump;
    public AudioClip bounce;
    public AudioClip fall;
    public AudioClip win;
    public AudioClip gameOver;
    public AudioClip checkpoint;
    public AudioClip hit;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        Instance = this;

        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        if (music != null) musicSource.Play();
    }

    public static void Play(Sfx type, float volume = 1f)
    {
        if (Instance == null) return;

        AudioClip clip = null;
        switch (type)
        {
            case Sfx.Coin: clip = Instance.coin; break;
            case Sfx.Jump: clip = Instance.jump; break;
            case Sfx.Bounce: clip = Instance.bounce; break;
            case Sfx.Fall: clip = Instance.fall; break;
            case Sfx.Win: clip = Instance.win; break;
            case Sfx.GameOver: clip = Instance.gameOver; break;
            case Sfx.Checkpoint: clip = Instance.checkpoint; break;
            case Sfx.Hit: clip = Instance.hit; break;
        }

        if (clip != null) Instance.sfxSource.PlayOneShot(clip, volume);
    }
}