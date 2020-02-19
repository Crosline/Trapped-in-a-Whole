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

    public void GenerateLevelObject()
    {

    }

    public GameLevel(int SceneID)
    {
        this.SceneID = SceneID;
        LoadLevel();
        var obj = GameObject.FindGameObjectsWithTag("Shard");
        LevelShards = obj.Length;
        Player.Instance.CurrentLevel = this;
    }
}