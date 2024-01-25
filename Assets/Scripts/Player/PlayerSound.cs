using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip engineIdleSound;

    [SerializeField]
    private AudioClip engineDrivingSound;

    [SerializeField]
    private AudioClip shotFiringSound;

    [SerializeField]
    private AudioClip tankExplosionSound;

    private AudioSource engineAudioSource;
    private AudioSource effectsAudioSource;

    private bool isDriving;

    private void Start()
    {
        engineAudioSource = GetComponents<AudioSource>()[0];
        effectsAudioSource = GetComponents<AudioSource>()[1];

        ChangeEngineSound(engineIdleSound);
        engineAudioSource.Play();
    }

    public void StopEngineSound()
    {
        engineAudioSource.Stop();
    }

    public void ChangeEngineSound(bool isDriving)
    {
        if (this.isDriving == isDriving)
            return;

        if (isDriving)
        {
            engineAudioSource.clip = engineDrivingSound;
            engineAudioSource.Play();
        }
        else
        {
            engineAudioSource.clip = engineIdleSound;
            engineAudioSource.Play();
        }

        this.isDriving = isDriving;
    }
    
    public void PlayFiringSound()
    {
        effectsAudioSource.PlayOneShot(shotFiringSound);
    }
}
