using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] Slider M_Slider;
    [SerializeField] Slider S_Slider;

    [SerializeField] List<AudioSource> AudioList;

    private float MusicVolume = 0.5f;
    private float SFXVolume = 0.5f;

    public void OnMusicVolumeChanged()
    {
        MusicVolume = M_Slider.value;
    }

    public void OnSFXVolumeChanged()
    {
        SFXVolume = S_Slider.value;
    }

    public void OnSave()
    {
        foreach (var item in AudioList)
        {
            if (item.playOnAwake) item.volume = MusicVolume;
            else item.volume = SFXVolume;
        }
    }
}
