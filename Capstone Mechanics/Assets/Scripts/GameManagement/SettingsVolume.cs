using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer fxMixer;


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume + 5);
    }
    public void SetFXVolume(float volume)
    {
        fxMixer.SetFloat("volume", volume + 5);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
