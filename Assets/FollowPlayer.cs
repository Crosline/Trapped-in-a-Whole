using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
