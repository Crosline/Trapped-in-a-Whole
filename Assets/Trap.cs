using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y > transform.position.y)
                StartCoroutine(Player.Instance.Die());
        }
    }

}
