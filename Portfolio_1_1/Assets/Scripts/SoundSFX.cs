using UnityEngine;

public class SoundSFX : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _volume;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundSFX()
    {
        _audioSource.PlayOneShot(_audioClip, _volume);
    }
}
