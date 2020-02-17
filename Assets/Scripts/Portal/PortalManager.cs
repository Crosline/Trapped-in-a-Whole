﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour {

    public Transform[] p;

    void Start() {

        GameObject portal1 = GameObject.Find("Portal1");
        GameObject portal2 = GameObject.Find("Portal2");
        GameObject portal3 = GameObject.Find("Portal3");


        if (SceneManager.GetActiveScene().buildIndex < 2 || SceneManager.GetActiveScene().buildIndex > SceneManager.sceneCountInBuildSettings - 1) {
            Destroy(portal1);
            Destroy(portal2);
            Destroy(portal3);
        }

        if (portal1 == null)
            return;

        if (p[0] != null)
            portal1.transform.position = p[0].position;
        if (p[1] != null)
            portal2.transform.position = p[1].position;
        if (p[2] == null && portal3 != null)
            portal3.transform.position = new Vector2(-10000, -10000);
        else if (portal3 != null)
            portal3.transform.position = p[1].position;

    }
}
