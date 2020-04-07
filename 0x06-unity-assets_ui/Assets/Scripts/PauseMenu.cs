using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Manages the pause menu.</summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>The stage's pause menu.</summary>
    public Canvas pauseMenu;

    // saved Time.timeScale value
    private float timeScale;

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

    // Check for the user to activate the menu.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.Pause();
        }
    }
}
