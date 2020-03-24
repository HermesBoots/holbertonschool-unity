using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manipulates the on-screen timer.</summary>
public class Timer : MonoBehaviour
{
    /// <summary>The text UI element to update.</summary>
    public Text timerText;
    
    /// <summary>Update the timer each frame.</summary>
    void Update()
    {
        this.timerText.text = string.Format("{0}:{1:00.00}", Mathf.RoundToInt(Time.time / 60), Time.time % 60);
    }
}
