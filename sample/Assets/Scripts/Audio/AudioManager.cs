using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    [SerializeField] private string sceneToDestroy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip musicClip, float volume = 1.0f, float fadeDuration = 1.0f)
    {
        StartCoroutine(FadeMusic(musicClip, volume, fadeDuration));
    }

    private IEnumerator FadeMusic(AudioClip newClip, float targetVolume, float fadeDuration)
    {
        if (audioSource.isPlaying)
        {
            // Fade out
            while (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime / fadeDuration;
                yield return null;
            }
            audioSource.Stop();
        }

        // Change clip and fade in
        audioSource.clip = newClip;
        audioSource.Play();
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
    public void StopMusic()
    {
        // Assuming you have an AudioSource for music playback
        AudioSource musicSource = GetComponent<AudioSource>();
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneToDestroy)
        {
            Destroy(gameObject);
        }
    }

}