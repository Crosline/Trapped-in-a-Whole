using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour {



    public static PortalManager Instance;


    public Transform[] p;



    GameObject portal1;
    GameObject portal2;
    GameObject portal3;
    GameObject portal4;

    private void Awake() => Instance = this;
    void Start() {

        portal1 = GameObject.Find("Portal1");
        portal2 = GameObject.Find("Portal2");
        portal3 = GameObject.Find("Portal3");
        portal4 = GameObject.Find("Portal4");


        if (SceneManager.GetActiveScene().buildIndex < 1 || SceneManager.GetActiveScene().buildIndex > 20) {
            Destroy(portal1);
            Destroy(portal2);
            Destroy(portal3);
            Destroy(portal4);
        }
        if (portal1.GetComponent<TutorialPortal>() != null)
            return;
        PortalSetup();

    }



    public void ActivatePortals() {
        if (portal1.GetComponent<TutorialPortal>() != null) {
            portal1.GetComponent<TutorialPortal>().ActivateMe();
            portal2.GetComponent<TutorialPortal>().ActivateMe();
        }
        else {
            portal1.GetComponent<Portal>().ActivateMe();
            portal2.GetComponent<Portal>().ActivateMe();
            portal3.GetComponent<Portal>().ActivateMe();
            portal4.GetComponent<Portal>().ActivateMe();
        }
    }





    void PortalSetup() {

        if (Random.Range(0, 2) == 0) {

            if (p.Length == 2) {
                portal1.name = "Portal2";
                portal2.name = "Portal1";
                // red good
                // blue bad
            } else if (p.Length == 3) {
                portal1.name = "Portal2";
                portal2.name = "Portal3";
                portal3.name = "Portal1";
                // red good
                // blue bad
                // green good
            } else if (p.Length == 4) {
                portal1.name = "Portal4";
                portal2.name = "Portal3";
                portal3.name = "Portal2";
                portal4.name = "Portal1";
                // red good
                // blue bad
                // green bad
                // yellow good
            }
        } else {
            /*
             * else condition is
             * blue good
             * red bad
             * green good
             * yellow bad
            */
        }


        if (portal1 == null)
            return;

        if (p.Length >= 2) {
            portal1.transform.position = p[0].position;
            portal1.transform.rotation = p[0].rotation;

            portal2.transform.position = p[1].position;
            portal2.transform.rotation = p[1].rotation;

            portal3.transform.position = new Vector2(-10000, -10000);
            portal4.transform.position = new Vector2(-10000, -10000);

        }
        if (p.Length == 3) {

            portal3.transform.position = p[2].position;
            portal3.transform.rotation = p[2].rotation;

        }
        if (p.Length == 4) {

            portal4.transform.position = p[3].position;
            portal4.transform.rotation = p[3].rotation;

            portal3.transform.position = p[2].position;
            portal3.transform.rotation = p[2].rotation;

        }


        portal1.GetComponent<Portal>().CreatePortal();
        portal2.GetComponent<Portal>().CreatePortal();
        portal3.GetComponent<Portal>().CreatePortal();
        portal4.GetComponent<Portal>().CreatePortal();



        portal1.GetComponent<BoxCollider2D>().enabled = false;
        portal2.GetComponent<BoxCollider2D>().enabled = false;
        portal3.GetComponent<BoxCollider2D>().enabled = false;
        portal4.GetComponent<BoxCollider2D>().enabled = false;


    }







}
