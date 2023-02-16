using System;
using UnityEngine;
using UnityEngine.Events;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDamageByWizard;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private UnityEvent _onHeal;
    [SerializeField] private HealthChangeEvent _onChange;

    private Hero _hero;

    private void Awake()
    {
        _hero = GetComponent<Hero>();
    }

    public void ModifyHeroHealth(int healthDelta)
    {
        _health += healthDelta;
        _onChange?.Invoke(_health);

        if (healthDelta > 0)
        {
            _onHeal?.Invoke();
        }
        if (healthDelta < 0 && !_hero.m_combatIdle)
        {
            _onDamage?.Invoke();
        }

        if (healthDelta < 0 && !_hero.m_forceField)
        {
            _onDamageByWizard?.Invoke();
        }

        if (_health <= 0)
        {
            _onDie?.Invoke();
        }
    }

    public void SetHealth(int health)
    {
        _health = health;
    }

    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {
    }
}
