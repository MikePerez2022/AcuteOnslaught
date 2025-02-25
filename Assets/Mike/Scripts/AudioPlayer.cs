using System.Collections.Generic;
using UnityEngine;

public enum SoundTypes{
    PICKUP,
    GUN1,
    GUN2,
    GUN3,
    GUN4,
    GUN5,
    GUN6,
    GUN7,
    GUN8
}

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] soundList;
    private static AudioPlayer instance;

    private void Awake()
    {
        instance = this;
        
    }

    public static void PlaySound(SoundTypes sound)
    {
        instance.soundList[(int)sound].Play();
    }

    public static void PlaySound(int sound)
    {
        instance.soundList[sound].Play();
    }
}
