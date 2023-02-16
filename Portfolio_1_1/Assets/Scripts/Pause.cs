using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public float timer;
    public bool isPause = false;
    public bool guiPause;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _lastSaveButton;
    [SerializeField] private Button _mainMenuButton;

    private void Update()
    {
        Time.timeScale = timer;

        if (Input.GetKeyDown(KeyCode.Escape) && isPause == false)
        {
            isPause = true;
            ActivatePauseMenu();
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause == true)
        {
            isPause = false;
            ExitPauseMenu();
            Cursor.visible = false;
        }

        if (isPause == true)
        {
            timer = 0; 
            guiPause = true;
        }
        else if (isPause == false)
        {
            timer = 1f; 
            guiPause = false;
        }
    }
    public void ActivatePauseMenu()
    {
        _restartButton.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(true);
        _lastSaveButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);
    }

    public void ExitPauseMenu()
    {
        _restartButton.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(false);
        _lastSaveButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
    }

    public void ContinueButton()
    {
        isPause = false;
        ExitPauseMenu();
        Cursor.visible = false;
    }

    public void RestartGameButton()
    {
        SceneManager.LoadScene("Scene1");
        isPause = false;
        Cursor.visible = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }
}
