using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>Contains callbacks for the victory screen.</summary>
public class WinMenu : MonoBehaviour
{
    /// <summary>Return to the main menu.</summary>
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>Go to the next level.</summary>
    public void Next() {
        int number;

        number = Int32.Parse(SceneManager.GetActiveScene().name.Substring(5));
        if (number < 3)
            SceneManager.LoadScene(String.Format("Level{0:D2}", number + 1));
        else
            SceneManager.LoadScene("MainMenu");
    }
}
