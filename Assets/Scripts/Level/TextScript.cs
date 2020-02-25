using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < 3)
            gameObject.GetComponent<MeshRenderer>().enabled = true;

        else
            gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
