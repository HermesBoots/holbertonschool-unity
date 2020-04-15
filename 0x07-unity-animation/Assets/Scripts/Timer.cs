using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manipulates the on-screen timer.</summary>
public class Timer : MonoBehaviour
{
    /// <summary>The text UI element to update when the level ends.</summary>
    public Text finalTime;

    /// <summary>The text UI element to update.</summary>
    public Text timerText;

    // whether the timer is running
    private bool stopped = false;
    
    /// <summary>Stop the timer when needed.</summary>
    public void Stop() {
        this.stopped = true;
        this.timerText.color = Color.green;
        this.timerText.fontSize = 60;
    }

    /// <summary>Show the final time when the level ends.</summary>
    public void Win() {
        this.Stop();
        this.finalTime.text = this.timerText.text;
        this.timerText.enabled = false;
    }

    /// <summary>Update the timer each frame.</summary>
    private void Update() {
        if (!this.stopped)
            this.timerText.text = string.Format(
                "{0}:{1:00.00}",
                Mathf.RoundToInt(Time.time / 60),
                Time.time % 60
            );
    }
}
