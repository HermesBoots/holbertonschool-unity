using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>Controls the camera animations and transitions back to gameplay.</summary>
public class CutsceneController : MonoBehaviour
{
    /// <summary>The player object to toggle.</summary>
    public PlayerController player;
    /// <summary>The main camera to toggle.</summary>
    public Camera myCamera;
    /// <summary>The timer to toggle.</summary>
    public Canvas timer;

    // choose the correct animation to play
    private void Start() {
        int number;

        number = Int32.Parse(SceneManager.GetActiveScene().name.Substring(5));
        this.GetComponent<Animator>().Play(String.Format("Intro{0:D2}", number));
    }

    /// <summary>Return control to the game when the cutscene ends.</summary>
    public void Finished() {
        this.player.enabled = true;
        this.myCamera.gameObject.SetActive(true);
        this.timer.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
