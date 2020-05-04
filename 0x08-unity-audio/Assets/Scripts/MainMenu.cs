using System;
using UnityEngine;
using UnityEngine.Audio;


/// <summary>Contains button callbacks for the main menu.</summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>Mixer to adjust volume of.</summary>
    public AudioMixer mixer;

    /// <summary>Quit the game.</summary>
    public void Exit() {
        Debug.Log("Exited");
        Application.Quit();
    }

    /// <summary>Load the level corresponding to a pressed button.</summary>
    /// <param name="level">Which level to load.</param>
    public void LevelSelect(int level) {
        OptionsMenu.MyLoadScene(String.Format("Level{0:D2}", level));
    }

    /// <summary>Load the options menu.</summary>
    public void Options() {
        OptionsMenu.MyLoadScene("Options");
    }

    // Adjust volume when starting the game
    private void Start() {
        if (PlayerPrefs.HasKey("BGMVol"))
            this.mixer.SetFloat("BGMVol", PlayerPrefs.GetFloat("BGMVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("BGMVol")) : -144);
        if (PlayerPrefs.HasKey("SFXVol")) {
            this.mixer.SetFloat("RunningVol", PlayerPrefs.GetFloat("SFXVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("SFXVol")) : -144);
            this.mixer.SetFloat("LandingVol", PlayerPrefs.GetFloat("SFXVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("SFXVol")) : -144);
            this.mixer.SetFloat("AmbientVol", PlayerPrefs.GetFloat("SFXVol") != 0 ? 20 * Mathf.Log10(PlayerPrefs.GetFloat("SFXVol")) : -144);
        }
    }
}
