using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>Handles victory conditions.</summary>
public class WinTrigger : MonoBehaviour
{
    /// <summary>Victory UI.</summary>
    public Canvas winScreen;

    /// <summary>Sound that plays when beating the level.</summary>
    public AudioClip winSound;

    // Stop the timer when the player hits the end.
    private void OnTriggerEnter(Collider other) {
        if (other.name.Equals("Player")) {
            this.winScreen.gameObject.SetActive(true);
            GameObject.Find("Player").GetComponent<Timer>().Win();
        }
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = this.winSound;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
    }
}
