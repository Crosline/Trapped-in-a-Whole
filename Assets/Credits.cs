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

    IEnumerator StartAgain() {
        yield return new WaitForSeconds(15f);

        new GameLevel(1);
        yield break;
    }

}
