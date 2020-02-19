using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;

    [SerializeField] private AudioClip mainSong;

    private void Awake()
    {
        Instance = this;
    }
}