using UnityEngine;
using UnityEngine.SceneManagement;


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

    /// <summary>Return to the previous scene.</summary>
    public void Back() {
        SceneManager.LoadScene(OptionsMenu.lastScene);
    }
}
