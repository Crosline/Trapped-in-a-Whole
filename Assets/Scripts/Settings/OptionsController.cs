using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour {

    IEnumerator Start() {

        while (GameSettings.Instance.IsReady) {



            yield return null;
        }






    }

}
