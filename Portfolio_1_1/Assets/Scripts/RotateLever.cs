using UnityEngine;

public class RotateLever : MonoBehaviour
{
    [SerializeField] private GameObject _objectToRotate;
    [SerializeField] private float _angleX;
    [SerializeField] private float _angleY;
    [SerializeField] private float _angleZ;
    [SerializeField] private AudioClip _leverDownSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void RotateLeverAngle()
    {
        transform.Rotate(_angleX, _angleY, _angleZ);
    }

    public void LeverSound()
    {
        _audioSource.PlayOneShot(_leverDownSound, 0.3f);
    }
}
