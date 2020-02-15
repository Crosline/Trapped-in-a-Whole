using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour {


    #region singleton

    private static GameSettings _instance;

    public static GameSettings Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameSettings>();
                if (_instance == null) {
                    GameObject obj = new GameObject("Singleton_GameSettings");
                    _instance = obj.AddComponent<GameSettings>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    #endregion


    public bool IsReady { get; private set; }

    public IList<string> qualitySettings { get; private set; }
    public IList<Resolution> ResolutionSettings { get; private set; }

    public UserOptions userOptions { get; private set; }

    void Awake() {
        qualitySettings = new List<string>(QualitySettings.names);
        ResolutionSettings = new List<Resolution>(Screen.resolutions);

        UserOptions = LoadOptions();

        IsReady = true;
    }

    UserOptions LoadOptions() {

    }

}
