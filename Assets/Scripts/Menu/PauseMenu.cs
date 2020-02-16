using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused;

    public GameObject pauseMenu;

    void Start() {

    }

    void Update() {

        if (Input.GetButtonDown("Cancel")) {
            if (isPaused)
                Resume();
            else
                Pause();
        }

    }

    #region Pause Methods

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    #endregion

    #region Scene Methods
    public void ReturnMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame() {
        Application.Quit();
    }


    #endregion

}
