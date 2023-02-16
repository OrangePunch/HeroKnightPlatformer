using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private float _coinValue;
    [SerializeField] private AudioClip _coinSound;

    private GameSession _session;
    private AudioSource _playerAudio;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _playerAudio = GetComponent<AudioSource>();
    }

    public void AddCoins()
    {
        if (_session != null)
        {
            _session.Data.Coins += _coinValue;
        }
    }

    public void CoinSound()
    {
        _playerAudio.PlayOneShot(_coinSound, 0.04f);
    }
}
