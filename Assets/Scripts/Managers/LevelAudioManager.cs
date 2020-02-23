using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAudioManager : MonoBehaviour
{
    public static LevelAudioManager Instance;

    [SerializeField] public float LastPlayingTime;
    [SerializeField] public float SoundVolumeChangeSpeed;

    [SerializeField] protected AudioSource _musicSource;
    [SerializeField] protected AudioSource _ambientSource;
    
    [SerializeField] private AudioClip[] _levelAmbients;
    [SerializeField] private AudioClip[] _levelMusic;

    private void Awake()
    {

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        StartCoroutine(SoundFade(true));
    }


    public IEnumerator SoundFade(bool turnOn)
    {
        Debug.Log("SOUND CHANGE");
        if (turnOn)
        {
            GetMusic();
            _musicSource.Play();
            _ambientSource.Play();
            _musicSource.time = LastPlayingTime;
            for (float i = 0; i<1; i += SoundVolumeChangeSpeed)
            {
                _ambientSource.volume = i;
                _musicSource.volume = i;
                yield return new WaitForFixedUpdate();
            }

        }
        else
        {
            LastPlayingTime = _musicSource.time;
            for (float i = 1; i > 0; i -= SoundVolumeChangeSpeed)
            {
                _ambientSource.volume = i;
                _musicSource.volume = i;
                yield return new WaitForFixedUpdate();
            }
            _musicSource.Stop();
            _ambientSource.Stop();
        }
    }

    private void GetMusic()
    {
        var lvl = SceneManager.GetActiveScene().buildIndex;
        if (lvl < 12)
        {
            _musicSource.clip = _levelMusic[0];
            _ambientSource.clip = _levelAmbients[0];
        }
        else if (lvl >= 12 && lvl < 15)
        {
            _musicSource.clip = _levelMusic[1];
            _ambientSource.clip = _levelAmbients[1];
        }
        else
        {
            _musicSource.clip = _levelMusic[2];
            _ambientSource.clip = _levelAmbients[2];
        }
    }
}