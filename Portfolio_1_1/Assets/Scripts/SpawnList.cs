using System;
using System.Linq;
using UnityEngine;

public class SpawnList : MonoBehaviour
{
    [SerializeField] private SpawnData[] _spawners;

    public void SpawnAll()
    {
        foreach (var spawnData in _spawners)
        {
            spawnData.Component.SpawnSomething();
        }
    }

    public void Spawn(string id)
    {
        var spawner = _spawners.FirstOrDefault(element => element.Id == id);
        spawner?.Component.SpawnSomething();
    }

    [Serializable]
    public class SpawnData
    {
        public string Id;
        public Spawn Component;
    }
}
