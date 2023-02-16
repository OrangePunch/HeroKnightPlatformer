using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void Exit()
    {
        var session = FindObjectOfType<GameSession>();
        session.Save();
        SceneManager.LoadScene(_sceneName);
    }
}
