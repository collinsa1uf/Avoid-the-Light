using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persist across scenes
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySoundFXClip(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayLoopingSound(AudioClip clip, AudioSource source, float volume = 1f)
    {
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.Play();
    }

    public void StopLoopingSound(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
}
