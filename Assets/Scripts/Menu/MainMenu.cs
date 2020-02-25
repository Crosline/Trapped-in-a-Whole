using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject Options;
    private bool mainMenu = true;

    public void PlayGame() {
        new GameLevel(6);
    }

    public void PlayTutorial() {
        new GameLevel(2);
    }

    public void PlayCredits() {
        new GameLevel(22);
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
