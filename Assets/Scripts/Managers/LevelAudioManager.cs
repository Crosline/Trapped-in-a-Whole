using System.Collections;
using UnityEngine;


public abstract class LevelAudioManager : MonoBehaviour
{
    public static LevelAudioManager Instance;

    public static float LastPlayingTime;
    public static float SoundVolumeChangeSpeed;

    [SerializeField] protected AudioSource _musicSource;
    [SerializeField] protected AudioSource _ambientSource;
    
    [SerializeField] private AudioClip _music;
    [SerializeField] private AudioClip _ambient;
    [SerializeField] private AudioClip[] levelEffects;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator SoundFade(bool turnOn)
    {
        if (turnOn)
        {
            _musicSource.SetScheduledStartTime(LastPlayingTime);
            _musicSource.Play();
            for (float i = 0; i<1; i-= SoundVolumeChangeSpeed)
            {
                _musicSource.volume = i;
                yield return new WaitForFixedUpdate();
            }

        }
        else
        {
            LastPlayingTime = _musicSource.time;
            for (float i = 1; i > 0; i -= SoundVolumeChangeSpeed)
            {
                _musicSource.volume = i;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}