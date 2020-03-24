using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>The camera attached to the player object.</summary>
    new public GameObject camera;

    /// <summary>The object's normal ground speed.</summary>
    public float speed = 24f;

    /// <summary>The maximum falling speed of this object.</summary>
    public float terminalFalling = 60f;

    // whether the player is touching the ground
    private bool grounded = true;
    // whether the player is rising from a jump
    private bool jumping = false;
    // when the player initiated a jump
    private float jumpTime = 0;

    /// <summary>Update the physics of the player each tick.</summary>
    protected void FixedUpdate() {
        float angle;
        KeyboardControls controls = this.GetComponent<KeyboardControls>();
        Action jump = null;
        Rigidbody body = this.GetComponent<Rigidbody>();
        Vector3 position = body.position;
        Vector3 velocity = Vector3.zero;

        if (this.grounded) {
            this.jumping = false;
            jump = controls.GetAction(Actions.Jump);
        }
        if (this.jumping && (Time.time - this.jumpTime > 0.5 || !controls.IsHeld(Actions.Jump)))
            this.jumping = false;

        angle = this.camera.transform.rotation.eulerAngles.y;
        if (controls.Direction == Actions.NE)
            angle += 45;
        else if (controls.Direction == Actions.E)
            angle += 90;
        else if (controls.Direction == Actions.SE)
            angle += 135;
        else if (controls.Direction == Actions.S)
            angle += 180;
        else if (controls.Direction == Actions.SW)
            angle += 225;
        else if (controls.Direction == Actions.W)
            angle += 270;
        else if (controls.Direction == Actions.NW)
            angle += 315;
        if (controls.Direction != Actions.None) {
            position.x += Mathf.Sin(Mathf.Deg2Rad * angle) * (this.speed / 120);
            position.z += Mathf.Cos(Mathf.Deg2Rad * angle) * (this.speed / 120);
        }
        if (position.y < -30)
            position.y = 30;

        if ((this.grounded && jump != null && jump.pressed) || (this.jumping && controls.IsHeld(Actions.Jump))) {
            this.jumping = true;
            if (this.grounded)
                this.jumpTime = Time.time;
            this.grounded = false;
            velocity.y = this.speed / 180 / Time.fixedDeltaTime;
        }
        else if (Mathf.Abs(body.velocity.y) > this.terminalFalling)
            velocity.y = Mathf.Sign(body.velocity.y) * this.terminalFalling;
        else
            velocity.y = body.velocity.y;
        body.velocity = velocity;
        body.MovePosition(position);
    }

    private void OnCollisionExit(Collision collision) {
        this.grounded = false;
    }
    private void OnCollisionEnter(Collision collision) {
        this.grounded = true;
    }

    /// <summary>Initialize when this game object loads.</summary>
    protected void Start()
    {
        this.gameObject.AddComponent<KeyboardControls>();
    }
}


/// <summary>The different actions available to the player.</summary>
public enum Actions
{
    None, N, E, S, W, NE, SE, SW, NW, Jump
}

/// <summary>Contains information about a player-triggered action.</summary>
public class Action
{
    public Actions action;
    public float time;
    public bool pressed;
    public bool released { get { return !this.pressed; } }
}


/// <summary>Contains methods and properties used for all kinds of controls (mobile/keyboard).</summary>
public interface IControls
{
    /// <summary>Get which direction is currently held, since multiple can be held at once.</summary>
    Actions Direction { get; }

    /// <summary>Get the last time the player triggered a game action.</summary>
    /// <param name="action">Which action to check.</param>
    /// <returns>Information about the action.</returns>
    Action GetAction(Actions action);

    /// <summary>Check whether the player is holding down an action.</summary>
    /// <param name="action">Which action to check.</param>
    /// <returns>Whether action is held.</returns>
    bool IsHeld(Actions action);
}


/// <summary>Implementation of keyboard controls.</summary>
public class KeyboardControls : MonoBehaviour, IControls
{
    // maximum amount of time an input can be buffered
    private const float bufferTime = 0.2f;

    // tracks which directions were previously held to make diagonals more natural
    private readonly Stack<Actions> directionStack;

    // which actions are currently being held by the player
    private readonly Dictionary<Actions, bool> heldActions;

    // a map of each game action to the user actions that continue it
    private readonly Dictionary<Actions, Func<bool>> holdTests;

    // a map of each game action to the user actions that trigger it
    private readonly Dictionary<Actions, Func<bool>> pressTests;

    // the last player interaction with an action that hasn't been used by the game
    private readonly Dictionary<Actions, Action> queue;

    /// <summary><see cref="IControls.Direction"/></summary>
    public Actions Direction { get { return this.directionStack.Peek(); } }

    /// <summary>Construct an instance and initialize complex fields.</summary>
    public KeyboardControls() {
        this.holdTests = new Dictionary<Actions, Func<bool>>(10) {
            { Actions.N, () => Input.GetKey(KeyCode.W) },
            { Actions.NE, () => Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) },
            { Actions.E, () => Input.GetKey(KeyCode.D) },
            { Actions.SE, () => Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) },
            { Actions.S, () => Input.GetKey(KeyCode.S) },
            { Actions.SW, () => Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) },
            { Actions.W, () => Input.GetKey(KeyCode.A) },
            { Actions.NW, () => Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) },
            { Actions.Jump, () => Input.GetKey(KeyCode.Space) }
        };
        this.pressTests = new Dictionary<Actions, Func<bool>>(10) {
            { Actions.N, () => Input.GetKeyDown(KeyCode.W) },
            { Actions.NE, () => (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.W)) },
            { Actions.E, () => Input.GetKeyDown(KeyCode.D) },
            { Actions.SE, () => (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.S)) || (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.D)) },
            { Actions.S, () => Input.GetKeyDown(KeyCode.S) },
            { Actions.SW, () => (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.A)) || (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.S)) },
            { Actions.W, () => Input.GetKeyDown(KeyCode.A) },
            { Actions.NW, () => (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.W)) },
            { Actions.Jump, () => Input.GetKeyDown(KeyCode.Space) }
        };
        this.directionStack = new Stack<Actions>(8);
        this.directionStack.Push(Actions.None);
        this.heldActions = new Dictionary<Actions, bool>(10);
        this.queue = new Dictionary<Actions, Action>(10);
        foreach (Actions action in Enum.GetValues(typeof(Actions))) {
            this.heldActions[action] = false;
            this.queue[action] = null;
        }
    }

    /// <summary><see cref="IControls.GetAction(Actions)"/></summary>
    /// <param name="action">Which action to check.</param>
    /// <returns>Info about the action.</returns>
    public Action GetAction(Actions action) {
        Action ret;
        ret = this.queue[action];
        this.queue[action] = null;
        if (ret != null)
        Debug.Log(ret.time);
        return ret;
    }

    /// <summary><see cref="IControls.IsHeld(Actions)"/></summary>
    /// <param name="action">Which action to check.</param>
    /// <returns>Whether action is held.</returns>
    public bool IsHeld(Actions action) {
        return this.heldActions[action];
    }

    /// <summary>Checks which buttons are pressed and which actions to trigger.</summary>
    private void Update() {
        foreach (Actions action in Enum.GetValues(typeof(Actions)))
            if (this.queue[action] != null && Time.time - this.queue[action].time > KeyboardControls.bufferTime)
                this.queue[action] = null;
        foreach (Actions action in Enum.GetValues(typeof(Actions))) {
            if (action == Actions.None)
                continue;
            if (this.pressTests[action]()) {
                this.queue[action] = new Action { action = action, time = Time.time, pressed = true };
                this.heldActions[action] = true;
                if (action != Actions.Jump)
                    this.directionStack.Push(action);
            } else if (this.heldActions[action] && !this.holdTests[action]()) {
                this.queue[action] = new Action { action = action, time = Time.time, pressed = false };
                this.heldActions[action] = false;
            }
        }
        while (this.directionStack.Count > 1) {
            if (this.heldActions[this.directionStack.Peek()])
                break;
            this.directionStack.Pop();
        }
    }
}