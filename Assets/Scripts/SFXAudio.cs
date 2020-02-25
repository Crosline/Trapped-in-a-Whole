using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SFXAudio : MonoBehaviour
{
    public static SFXAudio Instance;

    public AudioSource source1;
    public AudioSource source2;

    [SerializeField] AudioClip playerSteps;
    [SerializeField] AudioClip portalActivation;

    public void PlayPortal()
    {
        source1.Play();

    }

    private void Awake()
    {
        Instance = this;
        source1.clip = portalActivation;
    }

}

