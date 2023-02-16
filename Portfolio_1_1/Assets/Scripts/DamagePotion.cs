using UnityEngine;

public class DamagePotion : MonoBehaviour
{
    [SerializeField] private float _damageModifier;
    private HeroDamage _heroDamage;
    private GameSession _session;

    private void Awake()
    {
        _session = FindObjectOfType<GameSession>();
        _heroDamage = GameObject.Find("AttackRange").GetComponent<HeroDamage>();
    }

    public void AddDamage(GameObject target)
    {
        var damageComponent = target.GetComponent<HeroDamage>();
        if (damageComponent != null)
        {
            _session.Data.Damage += _damageModifier;
        }        
    }
}
