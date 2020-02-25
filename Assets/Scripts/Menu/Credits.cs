using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAgain());
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel"))
            new GameLevel(1);
    }

    IEnumerator StartAgain() {
        yield return new WaitForSeconds(10f);

        new GameLevel(1);
        yield break;
    }

}
