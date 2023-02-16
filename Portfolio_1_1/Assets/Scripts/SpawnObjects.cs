using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        var instance = Instantiate(_prefab, _target.position, Quaternion.identity);
        instance.SetActive(true);
    }
}
