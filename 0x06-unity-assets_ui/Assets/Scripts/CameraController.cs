using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Contains scripted actions for the main camera.</summary>
public class CameraController : MonoBehaviour
{
    /// <summary>Whether the camera Y-axis is inverted.</summary>
    public bool isInverted = false;

    /// <summary>Player object to attach to.</summary>
    public GameObject player;

    // Previous mouse position.
    private Vector3 mousePos;

    // check options on start
    private void Start() {
        if (PlayerPrefs.HasKey("InvertY"))
            this.isInverted = PlayerPrefs.GetInt("InvertY") == 0 ? false : true;
    }

    /// <summary>Move the camera to offset from player and allow rotation.</summary>
    void Update()
    {
        Vector2 angle;
        Vector3 player = this.player.transform.position;
        Vector3 position = Vector3.zero;

        if (Input.GetMouseButtonDown(1)) {
            this.mousePos = Input.mousePosition;
        } else if (Input.GetMouseButton(1)) {
            angle.y = (Input.mousePosition.x - this.mousePos.x) / Screen.width * 180;
            angle.x = (Input.mousePosition.y - this.mousePos.y) / Screen.height * 180;
            this.mousePos = Input.mousePosition;
            angle.x *= this.isInverted ? -1 : 1;
            this.transform.Rotate(new Vector3(0, angle.y), Space.World);
            if (Mathf.Abs(this.transform.rotation.eulerAngles.x % 90 + angle.x) < 90)
                this.transform.Rotate(new Vector3(angle.x, 0), Space.Self);
        }

        angle = this.transform.rotation.eulerAngles;
        position.x = player.x + Mathf.Sin(Mathf.Deg2Rad * angle.y) * -6.25f;
        position.y = player.y + Mathf.Sin(Mathf.Deg2Rad * -angle.x) * -6.25f;
        position.y += 1.25f;
        position.z = player.z + Mathf.Cos(Mathf.Deg2Rad * angle.y) * -6.25f;
        this.transform.position = position;
    }
}
