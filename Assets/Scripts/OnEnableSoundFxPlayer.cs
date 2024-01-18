using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OnEnableSoundFxPlayer: MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
