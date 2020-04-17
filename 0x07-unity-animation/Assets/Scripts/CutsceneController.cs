using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Controls the camera animations and transitions back to gameplay.</summary>
public class CutsceneController : MonoBehaviour
{
    /// <summary>The player object to toggle.</summary>
    public PlayerController player;
    /// <summary>The main camera to toggle.</summary>
    public Camera myCamera;
    /// <summary>The timer to toggle.</summary>
    public Canvas timer;

    /// <summary>Return control to the game when the cutscene ends.</summary>
    public void Finished() {
        this.player.enabled = true;
        this.myCamera.gameObject.SetActive(true);
        this.timer.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
