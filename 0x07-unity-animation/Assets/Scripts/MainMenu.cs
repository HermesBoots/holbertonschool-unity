using System;
using UnityEngine;


/// <summary>Contains button callbacks for the main menu.</summary>
public class MainMenu : MonoBehaviour
{
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
}
