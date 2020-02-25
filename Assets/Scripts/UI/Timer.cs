using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public static Timer Instance;
    
    private float time = 0;
    public TMPro.TextMeshProUGUI tmPro;
    public GameObject TimerCanvas;


    private bool timerOn;

    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(TimerCanvas);
        tmPro = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        if (timerOn) {
            time += Time.deltaTime;
        }
        UpdateTime();
    }

    private void UpdateTime() {

        float hour = time / 3600;
        float minute = (time % 3600) / 60;
        float second = time % 60;

        tmPro.SetText($"{(float)Math.Round(hour, 0)}:{(float)Math.Round(minute, 0)}:{(float)Math.Round(second, 0)}");
        //tmPro.SetText(String.Format($"{}:{}:{}%02d:%02d:%02d", hour, minute, second));

    }


    private void OnLevelWasLoaded(int level) {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 21) {
            timerOn = false;
        } else {
            timerOn = true;
        }
    }


}
