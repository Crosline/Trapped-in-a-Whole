using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public int lightYearChange = 2;

    private Animator anime;
    private string originalName;

    [SerializeField]
    private int whereToGo;

    //private Dictionary<int, int> pairs;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        originalName = gameObject.name;
        anime = GetComponent<Animator>();

        anime.SetBool("Active", false);
        //pairs = new Dictionary<int, int>();
        //CheckPortal();
        //CreatePortal();


    }

    void OnLevelWasLoaded(int level) {
        gameObject.name = originalName;
        anime.SetBool("Active", false);
        //CheckPortal();
        //CreatePortal();
    }

    /*private void CheckPortal() {

        int tempIndex = SceneManager.GetActiveScene().buildIndex;
        if (pairs.ContainsKey(tempIndex)) {
            SetDestination(pairs[tempIndex]);
        } else {
            CreatePortal();
        }

    }*/

    public void CreatePortal() {

        int newWorld = -1;

        while (newWorld == -1) {
            if (gameObject.name == "Portal1") {
                newWorld = Random.Range(SceneManager.GetActiveScene().buildIndex + 2, SceneManager.GetActiveScene().buildIndex + lightYearChange + 1);
            } else if (gameObject.name == "Portal2") {
                newWorld = Random.Range(SceneManager.GetActiveScene().buildIndex - lightYearChange, SceneManager.GetActiveScene().buildIndex - 1);
            } else if (gameObject.name == "Portal3") {
                newWorld = Random.Range(SceneManager.GetActiveScene().buildIndex + 1, SceneManager.GetActiveScene().buildIndex + lightYearChange);
            } else {
                newWorld = Random.Range(SceneManager.GetActiveScene().buildIndex - lightYearChange + 1, SceneManager.GetActiveScene().buildIndex);
            }

            if (newWorld >= 21) {
                newWorld = SceneManager.GetActiveScene().buildIndex + 1;
                break;
            } else if (newWorld <= 9) {
                newWorld = SceneManager.GetActiveScene().buildIndex - 1;
                break;
            }
        }


        SetDestination(newWorld);

        //pairs.Add(SceneManager.GetActiveScene().buildIndex, newWorld);

    }


    private void SetDestination(int i) {
        whereToGo = i;
    }

    public void ActivateMe() {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        anime.SetBool("Active", true);
        anime.SetTrigger("Activate");
    }


    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Hello");
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Player");
            new GameLevel(whereToGo);
            
        }
    }
}
