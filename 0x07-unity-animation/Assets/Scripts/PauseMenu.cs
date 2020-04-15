using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>Manages the pause menu.</summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>The stage's pause menu.</summary>
    public Canvas pauseMenu;

    // saved Time.timeScale value
    private float timeScale;

    /// <summary>Load the main menu.</summary>
    public void MainMenu() {
        Time.timeScale = this.timeScale;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>Load the options menu.</summary>
    public void Options() {
        Time.timeScale = this.timeScale;
        OptionsMenu.MyLoadScene("Options");
    }

    /// <summary>Toggle the pause menu.</summary>
    public void Pause() {
        if (this.pauseMenu.gameObject.activeSelf) {
            this.pauseMenu.gameObject.SetActive(false);
            this.GetComponent<Timer>().enabled = true;
            this.GetComponent<PlayerController>().enabled = true;
            Time.timeScale = this.timeScale;
        }
        else {
            this.pauseMenu.gameObject.SetActive(true);
            this.GetComponent<Timer>().enabled = false;
            this.GetComponent<PlayerController>().enabled = false;
            this.timeScale = Time.timeScale;
            Time.timeScale = 0;
        }
    }

    /// <summary>Reload the current scene.</summary>
    public void Restart() {
        Time.timeScale = this.timeScale;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Check for the user to activate the menu.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.Pause();
        }
    }
}
