using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour 
{
	// Start is called before the first frame update
	void Start() {
<<<<<<< HEAD

		if (Random.Range(0, 2) == 0)
			new GameLevel(4);
		else
			new GameLevel(5);


=======
		new GameLevel(0);
>>>>>>> fc856023b92315781aa92ceb1e260d21084e45f9
	}
}
