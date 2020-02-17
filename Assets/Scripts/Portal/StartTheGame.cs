using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour {


	// Start is called before the first frame update
	void Start() {
		SceneManager.LoadScene("level1-3");
	}


}
