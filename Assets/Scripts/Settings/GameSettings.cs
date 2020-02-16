using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameSettings : MonoBehaviour {


    #region I am a SINGLETON!

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


    static readonly string SETTINGS_FILE = "settings.json";

    public bool IsReady { get; private set; }

    public List<string> qualitySettings { get; private set; }
    public List<Resolution> resolutionSettings { get; private set; }

    public UserOptions userOptions { get; private set; }




    void Awake() {
        qualitySettings = new List<string>(QualitySettings.names);

        resolutionSettings = new List<Resolution>(Screen.resolutions);

        userOptions = LoadOptions();

        IsReady = true;
    }


    #region Public Methods

    public void SaveSettings(int q, int w, int h, bool fs, float ma, float mu, float fx) {
        var settings = new UserOptions {
            quality = q,
            fullScreen = fs,
            width = w,
            height = h,
            masterVolume = ma,
            musicVolume = mu,
            effectsVolume = fx
        };

        string fullPath = Path.Combine(Application.persistentDataPath, SETTINGS_FILE);

        if (File.Exists(fullPath)) {
            File.Delete(fullPath);
        }

        File.WriteAllText(fullPath, JsonUtility.ToJson(settings));
        ApplySettings(settings);

        userOptions = settings;

    }

    #endregion



    #region Private Methods

    UserOptions LoadOptions() {
        string fullPath = Path.Combine(Application.persistentDataPath, SETTINGS_FILE);

        if (File.Exists(fullPath)) {
            string json = File.ReadAllText(fullPath);
            var settings = JsonUtility.FromJson<UserOptions>(json);
            ApplySettings(settings);
            return settings;
        } else {
            return new UserOptions() {
                quality = QualitySettings.GetQualityLevel(),
                fullScreen = Screen.fullScreen,
                width = Screen.width,
                height = Screen.height,
                masterVolume = 0,
                musicVolume = 0,
                effectsVolume = 0
};
        }
    }


    void ApplySettings(UserOptions settings) {
        QualitySettings.SetQualityLevel(settings.quality);
        Screen.SetResolution(settings.width, settings.height, settings.fullScreen);
    }



    #endregion

}
