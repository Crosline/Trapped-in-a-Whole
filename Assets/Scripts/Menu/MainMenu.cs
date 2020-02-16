using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject Options;
    private bool mainMenu = true;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KillTheGame() {
        Application.Quit();
    }

    public void OpenOptions() {
        Options.SetActive(mainMenu);
        gameObject.SetActive(!mainMenu);
        mainMenu = !mainMenu;
    }

}
