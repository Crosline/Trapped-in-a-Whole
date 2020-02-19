using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel
{
    public int SceneID;
    public int ShardsAmmount;
    public int ShardsCollected;

    public void LoadLevel() => SceneManager.LoadScene(SceneID);

    public GameLevel(int SceneID)
    {

    }
}