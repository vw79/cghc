using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Slider brightnessSlider;
    public Slider volumeSlider;
    public Slider musicSlider;
    public Slider soundefSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    public AudioMixer audioMixer;

    AutoExposure exposure;

    void Start()
    {
        brightness.TryGetSettings(out exposure);

        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 0.05f);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        float savedMusic = PlayerPrefs.GetFloat("Music", 1.0f);
        float savedSoundef = PlayerPrefs.GetFloat("SoundEffects", 1.0f);

        brightnessSlider.value = savedBrightness;
        AdjustBrightness(savedBrightness);

        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        musicSlider.value = savedMusic;
        SetMusicVolume(savedMusic);

        soundefSlider.value = savedSoundef;
        SetSoundefVolume(savedSoundef);
    }

    public void AdjustBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.05f;
        }

        PlayerPrefs.SetFloat("Brightness", exposure.keyValue.value);
        PlayerPrefs.Save();

        brightnessSlider.value = exposure.keyValue.value;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);

        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);

        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
    }

    public void SetSoundefVolume(float volume)
    {
        audioMixer.SetFloat("SoundEffects", volume);

        PlayerPrefs.SetFloat("SoundEffects", volume);
        PlayerPrefs.Save();
    }
}
