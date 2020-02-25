using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
                StartCoroutine(Player.Instance.Die());
        }
    }
}
