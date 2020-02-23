using System.Collections;
using UnityEngine;

public class WinGame : MonoBehaviour {

    public GameObject playerObject;

    public TMPro.TextMeshPro textNow;
    public MeshRenderer meshNow;

    public TMPro.TextMeshPro textFuture;
    public MeshRenderer meshFuture;

    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            StartCoroutine(AfterDialogue());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine(Dialogue());
        }
    }

    IEnumerator AfterDialogue() {

        playerObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        playerObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        playerObject.GetComponent<Animator>().SetFloat("Speed", 1);

        playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4 * 1, 0);

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4 * 1, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<Animator>().SetFloat("Speed", 1);

        yield break;
    }

    IEnumerator Dialogue() {
        playerObject.GetComponent<Animator>().SetFloat("Speed", 0);
        playerObject.GetComponent<Player>().enabled = false;
        playerObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, -180, 0);

        yield return new WaitForSeconds(1);
        meshNow.enabled = true;
        yield return new WaitForSeconds(3);
        meshNow.enabled = false;

        yield return new WaitForSeconds(1);
        meshFuture.enabled = true;
        yield return new WaitForSeconds(3);
        meshNow.enabled = false;

        yield return new WaitForSeconds(1);
        textNow.text = "...";
        meshNow.enabled = true;
        yield return new WaitForSeconds(2);
        meshNow.enabled = false;
        textFuture.text = "...";
        meshFuture.enabled = true;

        yield return new WaitForSeconds(2);
        //portal activate
        meshFuture.enabled = false;
        meshNow.enabled = true;
        textNow.text = "Now that's what I call a PLOT HOLE. Alright, let's get out of here!";

        yield return new WaitForSeconds(4);
        meshNow.enabled = false;

        playerObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        playerObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        playerObject.GetComponent<Animator>().SetFloat("Speed", 1);

        playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4 * 1, 0);

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4 * 1, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<Animator>().SetFloat("Speed", 1);


        yield break;
    }



}
