using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel
{
    public int SceneID;
    public int LevelShards;
    public int ShardsCollected
    {
        get => _shardsCollected;
        set
        {
            _shardsCollected = value;
            
            // if all shards collected, you your portal system
            if (_shardsCollected == LevelShards)
            {
                // Activate Portal
            }
        }
    }
    private int _shardsCollected;

    public void LoadLevel() => SceneManager.LoadScene(SceneID);

    public GameLevel(int SceneID)
    {
        this.SceneID = SceneID;
    }
}