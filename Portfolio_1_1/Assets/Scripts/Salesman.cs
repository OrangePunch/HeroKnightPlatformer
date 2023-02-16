using UnityEngine;

public class Salesman : MonoBehaviour
{
    [SerializeField] private GameObject _salesWindow;

    private float timer;
    private GameSession _session;

    private void Awake()
    {
        _session = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        Time.timeScale = timer;
    }
    public void ActivateSalesWindow()
    {
        _salesWindow.gameObject.SetActive(true);
        Cursor.visible = true;
        timer = 0;
    }

    public void ExitButton()
    {
        _salesWindow.gameObject.SetActive(false);
        timer = 1;
        Cursor.visible = false;
    }

    public void BuyDamagePotionButton()
    {
        if (_session.Data.Coins >= 25)
        {
            _session.Data.Damage -= 1;
            _session.Data.Coins -= 25;
        }
    }

    public void BuyHealthPotionButton()
    {
        if (_session.Data.Coins >= 10)
        {
            _session.Data.Hp += 5;
            _session.Data.Coins -= 10;
        }
    }
}
