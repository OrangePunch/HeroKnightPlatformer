using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    public void TeleportDestination(GameObject target)
    {
        target.transform.position = _destination.transform.position;
    }
}
