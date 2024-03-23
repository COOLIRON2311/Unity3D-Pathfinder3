﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class FlyCamera : MonoBehaviour
{
    /*
    Written by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.
    Converted to C# 27-02-13 - no credit wanted.
    Simple flycam I made, since I couldn't find any others made public.
    Made simple to use (drag and drop, done) for regular keyboard layout
    WASD : basic movement
    Shift : Makes camera accelerate
    Space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/

    /// <summary>
    /// regular speed
    /// </summary>
    readonly float mainSpeed = 25.0f;
    /// <summary>
    /// Multiplied by how long shift is held. Basically running.
    /// </summary>
    readonly float shiftAdd = 50.0f;
    /// <summary>
    /// Maximum speed when holding shift
    /// </summary>
    readonly float maxShift = 200.0f;
    /// <summary>
    /// How sensitive it with mouse
    /// </summary>
    readonly float camSens = 0.2f;
    /// <summary>
    /// Kind of in the middle of the screen, rather than at the top (play)
    /// </summary>
    private Vector3 lastMouse = new(0, 0, 0);
    private float totalRun = 1.0f;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }

        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;
        // Mouse camera angle done.

        // Keyboard commands
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = shiftAdd * totalRun * p;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p *= mainSpeed;
        }

        p *= Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        { //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(p);
        }
    }

    /// <summary>
    /// Returns the basic values, if it's 0 than it's not active.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
