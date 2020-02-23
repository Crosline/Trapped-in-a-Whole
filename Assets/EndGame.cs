using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision) {


        StartCoroutine(GameOver());

    }


    public IEnumerator GameOver() {

        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(GameplayUI.Instance.FadeEffect(true));
        yield return new WaitUntil(Waiting);
        yield return new WaitForSecondsRealtime(0.3f);
        new GameLevel(1);


        bool Waiting() => GameplayUI.Instance.isFading;
    }



}
