using UnityEngine;

public static class AudioPlayer
{
    public static AudioSource PlayClip2D(AudioClip clip)
    {
        GameObject audioObject = new GameObject("2DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = 0.5f;
        audioSource.Play();

        Object.Destroy(audioObject, clip.length);

        return audioSource;
    }
}
