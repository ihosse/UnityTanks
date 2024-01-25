using UnityEngine;
using UnityEngine.Audio;

public class BackGroundMusic : MonoBehaviour
{
    public AudioMixerSnapshot EndSnapShot => end;
    public AudioMixerSnapshot GameSnapShot => game;
    public AudioMixerSnapshot WinnerSnapShot => winner;

    [SerializeField]
    private AudioMixerSnapshot end;

    [SerializeField]
    private AudioMixerSnapshot game;

    [SerializeField]
    private AudioMixerSnapshot winner;

    [SerializeField]
    private AudioSource backgroundMusicSource;

    public void PlayMusic()
    {
        backgroundMusicSource.Play();
    }
    public void ChangeSnapShot(AudioMixerSnapshot snapshot, float timeToReach)
    {
        snapshot.TransitionTo(timeToReach);
    }
}
