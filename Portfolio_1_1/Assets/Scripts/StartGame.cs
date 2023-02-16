using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void StartGameMenu()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
