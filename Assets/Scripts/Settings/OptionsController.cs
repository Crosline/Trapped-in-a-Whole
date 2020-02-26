using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    [Header("Quality")]
    public TMPro.TMP_Dropdown quality;
    public TMPro.TMP_Dropdown resolution;
    public Toggle fullScreenToggle;
    [Header("Sliders")]
    public Slider master;
    public Slider music;
    public Slider effects;
    [Header("AudioMixers")]
    public AudioMixer masterMixer;
    private bool fullScreen;

    private float masterVolume;
    private float musicVolume;
    private float effectsVolume;

    public void FullScreen() {
        fullScreen = !fullScreen;
    }


    List<Resolution> filteredRes;

    public void ApplySettings() {
        Resolution res = filteredRes[resolution.value];
        int qual = quality.value;
        bool full = fullScreen;

        masterMixer.SetFloat("Master", masterVolume);
        masterMixer.SetFloat("Music", musicVolume);
        masterMixer.SetFloat("Effects", effectsVolume);


        GameSettings.Instance.SaveSettings(qual, res.width, res.height, full, masterVolume, musicVolume, effectsVolume);
    }

    #region volume

    public void MasterVolume() {
        masterVolume = master.value;
    }
    public void MusicVolume() {
        musicVolume = music.value;
    }
    public void EffectsVolume() {
        effectsVolume = effects.value;
    }

    #endregion


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


        master.value = user.masterVolume;
        music.value = user.musicVolume;
        effects.value = user.effectsVolume;

        masterMixer.SetFloat("Master", user.masterVolume);
        masterMixer.SetFloat("Music", user.musicVolume);
        masterMixer.SetFloat("Effects", user.effectsVolume);

    }

}
