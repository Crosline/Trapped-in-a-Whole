using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DelayedTrigger : MonoBehaviour {
    // Start is called before the first frame update


    public Transform playerCheck;
    public float checkRadius;
    public MeshRenderer ex;
    public LayerMask whatIsPlayer;
    public float waitForSec;

    public Animator anime;

    void Update() {

        if (Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer)) {
            ex.enabled = true;
            StartCoroutine(Trigger());
        }
        if (Physics2D.OverlapCircle(playerCheck.position, checkRadius - 0.2f, whatIsPlayer)) {
            //anime.Play("Trigger");

        }
    }

    public IEnumerator Trigger() {
        Debug.Log("ANAN");
        yield return new WaitForSeconds(waitForSec);
        anime.Play("Trigger");

        yield return new WaitForSeconds(waitForSec);
        ex.enabled = false;

        
        yield break;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCheck.position, checkRadius);

    }





}
