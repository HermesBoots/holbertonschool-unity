﻿using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>Contains operations performed in the options menu.</summary>
public class OptionsMenu : MonoBehaviour
{
    /// <summary>BGM audio group to adjust.</summary>
    public AudioMixer mixer;

    // name of last scene loaded
    private static string lastScene;

    /// <summary>Load a new scene and remember the previous one.</summary>
    /// <param name="name">Which scene to load next.</param>
    public static void MyLoadScene(string name) {
        OptionsMenu.lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }

    /// <summary>Save and apply current options.</summary>
    public void Apply() {
        PlayerPrefs.SetInt("InvertY", this.GetComponentInChildren<Toggle>().isOn ? 1 : 0);
        PlayerPrefs.SetFloat("BGMVol", GameObject.Find("BGMSlider").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("SFXVol", GameObject.Find("SFXSlider").GetComponent<Slider>().value);
        this.mixer.SetFloat("BGMVol", PlayerPrefs.GetFloat("BGMVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("BGMVol")) : -144);
        this.mixer.SetFloat("RunningVol", PlayerPrefs.GetFloat("SFXVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("SFXVol")) : -144);
        this.mixer.SetFloat("LandingVol", PlayerPrefs.GetFloat("SFXVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("SFXVol")) : -144);
        this.mixer.SetFloat("AmbientVol", PlayerPrefs.GetFloat("SFXVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("SFXVol")) : -144);
    }

    /// <summary>Return to the previous scene.</summary>
    public void Back() {
        SceneManager.LoadScene(OptionsMenu.lastScene);
    }

    // load settings on start
    private void Start() {
        if (PlayerPrefs.HasKey("InvertY"))
            this.GetComponentInChildren<Toggle>().isOn = PlayerPrefs.GetInt("InvertY") == 0 ? false : true;
        else
            this.GetComponentInChildren<Toggle>().isOn = false;
        if (PlayerPrefs.HasKey("BGMVol"))
            GameObject.Find("BGMSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("BGMVol");
        else
            GameObject.Find("BGMSlider").GetComponent<Slider>().value = 1;
        if (PlayerPrefs.HasKey("SFXVol"))
            GameObject.Find("SFXSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVol");
        else
            GameObject.Find("SFXSlider").GetComponent<Slider>().value = 1;
    }
}
