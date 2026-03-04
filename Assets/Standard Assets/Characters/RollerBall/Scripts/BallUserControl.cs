using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


namespace UnityStandardAssets.Vehicles.Ball
{
    public class BallUserControl : MonoBehaviour
    {
        private Ball      ball;         // Reference to the ball controller.
        private Vector3   move;         // The world-relative desired move direction, calculated from the camForward and user input.
        private Transform cam;          // A reference to the main camera in the scenes transform.
        private Vector3   camForward;	// The current forward direction of the camera.
        private bool      jump;         // whether the jump button is currently pressed.


        private void Awake()
        {
            // Set up the reference.
            ball = GetComponent<Ball>();

            // Get the transform of the main camera.
            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning("WARNING:  No main camera found.  Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // Use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
            }
			
        }	// Awake()


        private void Update()
        {
            // Get the axis and jump input.

            float h = CrossPlatformInputManager.GetAxis  ("Horizontal");
            float v = CrossPlatformInputManager.GetAxis  ("Vertical");
            jump    = CrossPlatformInputManager.GetButton("Jump");

            // Calculate move direction.
            if (cam != null)
            {
                // Calculate camera relative direction to move.
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                move = (v*camForward + h*cam.right).normalized;
            }
            else
            {
                // Use world-relative directions in the case of no main camera.
                move = (v*Vector3.forward + h*Vector3.right).normalized;
            }
			
        }	// Update()


        private void FixedUpdate()
        {
            // Call the Move function of the ball controller.
            ball.Move(move, jump);
            jump = false;
			
        }	// FixedUpdate()
		
    
	}	// class Ball
	
}	// namespace UnityStandardAssets.Vehicles.Ball
