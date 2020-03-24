using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Contains scripted actions for the main camera.</summary>
public class CameraController : MonoBehaviour
{
    /// <summary>Player object to attach to.</summary>
    public GameObject player;

    // Previous mouse position.
    private Vector3 mousePos;

    /// <summary>Move the camera to offset from player and allow rotation.</summary>
    void Update()
    {
        float angle;
        Vector3 player = this.player.transform.position;
        Vector3 position = Vector3.zero;

        if (Input.GetMouseButtonDown(1)) {
            this.mousePos = Input.mousePosition;
        } else if (Input.GetMouseButton(1)) {
            angle = (Input.mousePosition.x - this.mousePos.x) / Screen.width * 180;
            this.mousePos = Input.mousePosition;
            this.transform.Rotate(new Vector3(0, angle, 0), Space.World);
        }

        angle = this.transform.rotation.eulerAngles.y;
        position.x = player.x + Mathf.Sin(Mathf.Deg2Rad * angle) * -6.25f;
        position.y = player.y + 1.25f;
        position.z = player.z + Mathf.Cos(Mathf.Deg2Rad * angle) * -6.25f;
        this.transform.position = position;
    }
}
