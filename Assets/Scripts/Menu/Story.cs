using System.Collections;
using UnityEngine;

public class Story : MonoBehaviour {

    public Animator anime;
    public UnityEngine.UI.Text text;
    public UnityEngine.UI.Text text2;

    void Update() {

        if (Input.GetButtonDown("Cancel")) {
            new GameLevel(7);
        }



    }

    private void Start() {
        StartCoroutine(StoryGoesOn());
    }

    IEnumerator StoryGoesOn() {
        text2.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(3);
        text2.color = new Color(0.5f,0.5f,0.5f, 1);


        yield return new WaitUntil(Pressed);

        /*while (!Input.GetButtonDown("Jump")) {
            yield return new WaitForSeconds(0);
        }*/

        text2.color = new Color(1,1,1, 0);
        text.text = "So, Mr Astronaut took this entry exam for him to achieve his dream like the rest of us." +
            " Do all the boring stuff really like the air control, pressing the shiny button and let the thing fly." +
            " “Zoom zoom” it went no?  \n-<i>“Oh why yes, it went so fast that we landed lost in space!”</i> \n-<i>“Wait? We are lost in space?" +
            " Then how do we know this Mister Astronaut?”</i>";

        yield return new WaitForSeconds(3);
        text2.color = new Color(0.5f,0.5f,0.5f, 1);

        yield return new WaitUntil(Pressed);
        text2.color = new Color(1,1,1, 0);
        text.text = "-<i>“Because we are apart of him, you dimwit.. we are him! We are just part of his subconscious”</i>\n" +
            "Anyway, back to the story, so this Mr Astronaut ended up pressing the wrong button because the thing " +
            "fly zoom zoom away.We ended up crashing in this sucky sucky hole and ended up in a place we had never seen before. " +
            "It’s different from anything we had ever seen on earth. It was alien in a way, something out of the ordinary.";
        anime.SetTrigger("Boom");

        yield return new WaitForSeconds(1);
        text2.color = new Color(0.5f,0.5f,0.5f, 1);

        yield return new WaitUntil(Pressed);
        text2.color = new Color(1,1,1, 0);



        new GameLevel(7);
        yield break;


        bool Pressed() => Input.GetButtonDown("Jump");
    }



}
