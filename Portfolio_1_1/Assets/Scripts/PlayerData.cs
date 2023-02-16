using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float Coins;
    public float Hp;
    public float Damage;

    public PlayerData Clone()
    {
        var json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
