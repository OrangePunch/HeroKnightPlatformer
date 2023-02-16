using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Image _hpImage;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _coinsAmountText;
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _coinsAmount;
    [SerializeField] private Button _restartButton;

    public bool _isGameActive;

    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }

    public void UpdateHealth()
    {
        _hp = _session.Data.Hp;
        _hpText.text = "Health: " + _hp;
    }

    public void UpdateDamage()
    {
        _damage = -_session.Data.Damage;
        _damageText.text = "Damage: " + _damage;
    }

    public void UpdateCoinsAmount()
    {
        _coinsAmount = _session.Data.Coins;
        _coinsAmountText.text = "Coins: " + _coinsAmount;
    }

    public void GameOver()
    {
        _restartButton.gameObject.SetActive(true);
    }
}
