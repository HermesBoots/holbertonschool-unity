using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>Handles victory conditions.</summary>
public class WinTrigger : MonoBehaviour
{
    /// <summary>Victory UI.</summary>
    public Canvas winScreen;

    // Stop the timer when the player hits the end.
    private void OnTriggerEnter(Collider other) {
        if (other.name.Equals("Player")) {
            this.winScreen.gameObject.SetActive(true);
            GameObject.Find("Player").GetComponent<Timer>().Win();
        }
    }
}
