using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject Options;
    private bool mainMenu = true;

    public void PlayGame() {
        new GameLevel(5);
    }

    public void PlayTutorial() {
        new GameLevel(1);
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
