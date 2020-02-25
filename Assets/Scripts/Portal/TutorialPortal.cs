using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TutorialPortal : MonoBehaviour {

    private Animator anime;

    [SerializeField]
    private int whereToGo;

    //private Dictionary<int, int> pairs;

    void Awake() {
        anime = GetComponent<Animator>();

        anime.SetBool("Active", false);


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
