using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;

    [SerializeField] private Slider _thrustSlider;
    [SerializeField] private Text _shardsCount;
    [SerializeField] private Text _lightYear;
    [SerializeField] private float _fadeSpeed;
    public bool isFading;

    private void Awake() => Instance = this;

    public void ChangeThrustSliderValue(float value) =>
        _thrustSlider.value = value / Player.Instance.JetpackCapacity;

    public IEnumerator FadeEffect(bool fadeIn)
    {
        isFading = true;
        var scriptComponent = Camera.main.gameObject.GetComponent<ScreenTransitionImageEffect>();

        if (fadeIn)
        {
            scriptComponent.maskValue = 0;
            for (float i = 0; i<=1; i+=_fadeSpeed/100)
            {
                Debug.Log("Fade out!");
                scriptComponent.maskValue = i;
                yield return new WaitForFixedUpdate();
            }
            scriptComponent.maskValue = 1;
        }
        else
        {
            scriptComponent.maskValue = 1;
            for (float i = 1; i >= 0; i -= _fadeSpeed / 100)
            {
                Debug.Log("Fade out!");
                scriptComponent.maskValue = i;
                yield return new WaitForFixedUpdate();
            }
            scriptComponent.maskValue = 0;
        }

        yield return new WaitForSecondsRealtime(0.1f);
        isFading = false;
    }

    public void ChangeCollectedShards(int needToCollect, int alredyCollected) => _shardsCount.text = $"{alredyCollected}/{needToCollect}";
    public void ChangeLightYear(int scene) => _lightYear.text = $"You are {Mathf.Abs(scene - 19) * 100} light year away from home";

}