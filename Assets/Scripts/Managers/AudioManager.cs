using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    private void Awake()
    {
        Instance = this;
    }
}