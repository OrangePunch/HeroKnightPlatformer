using UnityEngine;

public class HeroDamage : MonoBehaviour
{
    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }

    public void ApplyHealthDelta(GameObject target)
    {
        var healthComponent = target.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.ModifyHealth(_session.Data.Damage);
        }
    }
}
