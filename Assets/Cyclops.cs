using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : Walker
{

    private bool isSpitting;
    public GameObject spit;

    private void Update() {
        if (CheckForPlayer())
            StartCoroutine(Spit());
    }

    IEnumerator Spit() {
        if (isSpitting)
            yield break;
        isSpitting = true;
        _animator.SetTrigger("isSpotted");
        yield return new WaitForSeconds(0.3f);
        Instantiate(spit, transform.position, Quaternion.identity);

        _animator.ResetTrigger("isSpotted");
        isSpitting = false;
        yield break;
    }


}
