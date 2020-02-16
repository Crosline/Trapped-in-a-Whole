using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {


    public TMPro.TMP_Dropdown quality;
    public TMPro.TMP_Dropdown resolution;
    public Toggle fullScreenToggle;
    private bool fullScreen;
    public void FullScreen() {
        fullScreen = !fullScreen;
    }


    List<Resolution> filteredRes;

    public void ApplySettings() {
        Resolution res = filteredRes[resolution.value];
        int qual = quality.value;
        bool full = fullScreen;

        GameSettings.Instance.SaveSettings(qual, res.width, res.height, full);
    }


    IEnumerator Start() {

        while (!GameSettings.Instance.IsReady) {
            yield return null;
        }

        var user = GameSettings.Instance.userOptions;

        quality.ClearOptions();
        quality.AddOptions(GameSettings.Instance.qualitySettings);
        quality.value = user.quality;



        

        List<string> res = new List<string>();
        filteredRes = new List<Resolution>();

        int w = -1;
        int h = -1;

        int index = 0;
        int currentResIndex = -1;

        foreach (var r in GameSettings.Instance.resolutionSettings) {
            if (w != r.width || h != r.height) { 
                string format = string.Format("{0}x{1}", r.width, r.height);
                res.Add(format);

                h = r.height;
                w = r.width;

                if (w == user.width && h == user.height)
                    currentResIndex = index;

                filteredRes.Add(r);

                index++;
            }
        }

        resolution.ClearOptions();
        resolution.AddOptions(res);
        resolution.value = currentResIndex;
        fullScreenToggle.isOn = user.fullScreen;
        fullScreen = user.fullScreen;
    }

}
