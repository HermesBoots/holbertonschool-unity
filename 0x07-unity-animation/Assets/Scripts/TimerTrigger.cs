using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Triggers the timer when needed.</summary>
public class TimerTrigger : MonoBehaviour
{
    /// <summary>Start the timer when the player moves.</summary>
    private void OnTriggerExit(Collider other) {
        if (other.name.Equals("Player"))
            other.GetComponent<Timer>().enabled = true;
    }
}
