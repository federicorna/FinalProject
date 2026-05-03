
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Musica")]
    [SerializeField] private AudioSource _musicSource;

    [Header("Tracce")]
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _storyMusic;
    [SerializeField] private AudioClip _level1Music;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  /// Non distrutto ai cambi di scena
    }

    //--[f.ni]--

    public void PlayMenuMusic() => PlayMusic(_menuMusic);
    public void PlayStoryMusic() => PlayMusic(_storyMusic);
    public void PlayLevel1Music() => PlayMusic(_level1Music);

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || _musicSource.clip == clip) return;
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void StopMusic() => _musicSource.Stop();
}