﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {
    [Header("Parameters")]
    // How fast the tank moves forward and back.
    public float m_Speed = 12f;  
    // How fast the tank turns in degrees per second.               
    public float m_TurnSpeed = 180f;     
    // The name of the input axis for moving forward and back       
    private string m_MovementAxisName;      
    // The name of the input axis for turning    
    private string m_TurnAxisName; 
    // Reference used to move the tank             
    private Rigidbody m_Rigidbody; 
    // The current value of the movement input             
    private float m_MovementInputValue; 
    // The current value of the turn input        
    private float m_TurnInputValue;             
    [Space]

    [Header("Shooting")]
    public GameObject bulletPrefab;

    private void Awake () {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }


    private void OnEnable () {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable () {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start () {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";
    }


    private void Update () {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis (m_TurnAxisName);
        Shoot(bulletPrefab);
    }

    private void FixedUpdate () {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move ();
        Turn ();
    }


    private void Move () {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn () {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }

    private void Shoot (GameObject projectile) {
        // If "Fire1" is pressed ...
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("shoot");
            Debug.Log(transform.forward);
            // Create Instance of the Bullet
            projectile = Instantiate(bulletPrefab);
            // Set the Bullet's rotation to the Tank's rotation
            projectile.transform.rotation = transform.rotation;
            // Set the Bullet's position to the front of the Tank's position
            projectile.transform.position = transform.position + (transform.forward * 2f);
            // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);
        }
    }
}
