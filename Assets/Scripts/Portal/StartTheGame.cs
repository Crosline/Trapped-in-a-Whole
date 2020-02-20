using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour 
{
	// Start is called before the first frame update
	void Start() {

		if (Random.Range(0, 2) == 0)
			new GameLevel(3);
		else
			new GameLevel(4);

	}
}
