using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel
{
    public int SceneID;

    public int LevelShards
    {
        get => _levelShards;
        set
        {
            _levelShards = value;
            GameplayUI.Instance.ChangeCollectedShards(value, ShardsCollected);
        }
    }
    private int _levelShards;

    public int ShardsCollected
    {
        get => _shardsCollected;
        set
        {
            _shardsCollected = value;
            GameplayUI.Instance.ChangeCollectedShards(LevelShards, value);

            // if all shards collected, you your portal system
            if (_shardsCollected == LevelShards)
            {
                PortalManager.Instance.ActivatePortals();
                // Activate Portal
            }
        }
    }
    private int _shardsCollected = 0;

    public void LoadLevel() => SceneManager.LoadScene(SceneID);

    public GameLevel(int SceneID)
    {
        this.SceneID = SceneID;
        LoadLevel();
        //Player.Instance.CurrentLevel = this;
        Player.CurrentLevel = this;
    }

    public void FindShards()
    {
        var obj = GameObject.FindGameObjectsWithTag("Shard");
        Debug.Log("Shards: " + obj.Length);
        LevelShards = obj.Length;
        GameplayUI.Instance.ChangeLightYear(SceneID);
    }
}