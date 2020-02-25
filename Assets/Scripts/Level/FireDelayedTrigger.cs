using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireDelayedTrigger : MonoBehaviour {


    private AudioSource source;
    [SerializeField] private AudioClip clip;

    public Transform playerCheck;
    public float checkRadius;
    public MeshRenderer ex;
    public LayerMask whatIsPlayer;
    public float waitForSec;

    public Animator anime;
    public Animator anime2;

    public bool isTriggered;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

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

        source.clip = clip;
        source.Play();

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
