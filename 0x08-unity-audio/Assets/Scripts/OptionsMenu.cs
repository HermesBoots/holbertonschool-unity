using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>Contains operations performed in the options menu.</summary>
public class OptionsMenu : MonoBehaviour
{
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
    }
}
