using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * 20);

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        anime.Play("boom");
        if (collision.gameObject.tag == "Player")
            StartCoroutine(Player.Instance.Die());
        Destroy(gameObject);
    }




}
