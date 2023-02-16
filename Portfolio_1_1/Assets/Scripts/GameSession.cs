using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    public PlayerData Data => _data;
    private PlayerData _save;

    private void Awake()
    {
        if (IsSessionExit())
        {
            Destroy(gameObject);
        }
        else
        {
            Save();
            DontDestroyOnLoad(this);
        }
    }

    private bool IsSessionExit()
    {
        var sessions = FindObjectsOfType<GameSession>();
        foreach (var gameSession in sessions)
        {
            if (gameSession != this)
            {
                return true;
            }
        }
        return false;
    }

    public void Save()
    {
        _save = _data.Clone();
    }

    public void LoadLastSave()
    {
        _data = _save.Clone();
    }
}
