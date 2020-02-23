using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireDelayedTrigger : MonoBehaviour {


    public Transform playerCheck;
    public float checkRadius;
    public MeshRenderer ex;
    public LayerMask whatIsPlayer;
    public float waitForSec;

    public Animator anime;
    public Animator anime2;

    public bool isTriggered;

    void Update() {

        if (Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer)) {
            ex.enabled = true;
            StartCoroutine(Trigger());
        }
    }

    public IEnumerator Trigger() {
        if (isTriggered)
            yield break;
        isTriggered = true;
        yield return new WaitForSeconds(waitForSec);
        anime.Play("Trigger");
        anime2.Play("Trigger");

        yield return new WaitForSeconds(waitForSec);
        ex.enabled = false;

        isTriggered = false;
        yield break;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCheck.position, checkRadius);

    }








}
