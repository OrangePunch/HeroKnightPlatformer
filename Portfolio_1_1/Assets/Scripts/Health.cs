using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private UnityEvent _onHeal;
    [SerializeField] private HealthChangeEvent _onChange;

    public void ModifyHealth(float healthDelta)
    {
        _health += healthDelta;
        _onChange?.Invoke(_health);

        if (healthDelta > 0)
        {
            _onHeal?.Invoke();
        }
        if (healthDelta < 0)
        {
            _onDamage?.Invoke();
        }

        if (_health <= 0)
        {
            _onDie?.Invoke();
        }
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    [Serializable]
    public class HealthChangeEvent : UnityEvent<float>
    {
    }
}

