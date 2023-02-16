using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;

    public void SpawnPrefabs()
    {
        foreach (var prefab in _prefabs)
        {
            prefab.gameObject.SetActive(true);
        }
    }
}
