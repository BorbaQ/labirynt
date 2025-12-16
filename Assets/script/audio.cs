using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;

    private int index = 0;

    private void Start()
    {
        PlayCurrent();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            Next();
        }
    }

    void PlayCurrent()
    {
        audioSource.clip = songs[index];
        audioSource.Play();
    }

    void Next()
    {
        index++;
        if (index >= songs.Length)
            index = 0;

        PlayCurrent();
    }
}
