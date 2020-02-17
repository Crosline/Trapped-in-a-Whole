using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;

    [SerializeField] private Slider _thrustSlider;

    private void Awake() => Instance = this;

    public void ChangeThrustSliderValue(float value)
    {
        _thrustSlider.value = value / Player.Instance.JetpackCapacity;
    }
}