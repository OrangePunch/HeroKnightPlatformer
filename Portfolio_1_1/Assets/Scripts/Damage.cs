using UnityEngine;

public class Damage : MonoBehaviour
{
    public float _hpDelta;

    public void ApplyHealthDelta(GameObject target)
    {
        var healthComponent = target.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.ModifyHealth(_hpDelta);
        }
    }
}

