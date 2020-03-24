using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles victory conditions.</summary>
public class WinTrigger : MonoBehaviour
{
    /// <summary>Stop the timer when the player hits the end.</summary>
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.name.Equals("Player"))
            other.GetComponent<Timer>().Stop();
    }
}
