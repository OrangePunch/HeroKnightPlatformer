using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;

    [ContextMenu("Spawn")]
    public void SpawnSomething()
    {
        var instance = Instantiate(_prefab, _target.position, Quaternion.identity);

        var scale = _target.lossyScale;
        instance.transform.localScale = scale;
        instance.SetActive(true);
    }
}
