using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DraculaController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float jumpStrength = 1.0f;

    public float rotationSpeed = 1.0f;
    public float verticalAngleLimit = 85.0f;

    //====Added variable for tracking # of jumps====
    public int jumpCount = 0;

    //===Crouching variables===
    public bool isCrouched = false;

    private Vector3 currentRotation;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        Rotate();
    }

    void MovePlayer()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        float y = rb.linearVelocity.y;  // Save the y velocity to maintain gravity effect

        if (Input.GetKey(KeyCode.W))
        {
            direction += Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Camera.main.transform.right;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed *= 1.007f;
        }

        // Normalize direction to prevent faster diagonal movement
        Vector3 velocity = direction.normalized * movementSpeed;
        velocity.y = y;  // Maintain original y velocity for gravity

        // Apply the velocity to the player
        rb.linearVelocity = velocity;
    }

    void Jump()
    {
        // Make sure jumpCount is reset if player is grounded (rb.velocity.y == 0)
        if (Mathf.Approximately(rb.linearVelocity.y, 0f))
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)  // Double jump logic
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jumpCount++;
        }
    }

    //void Crouch()
    //{
    //    if(Input.GetKey(KeyCode.C))
    //    {
    //        isCrouched = !isCrouched;
    //        if(!isCrouched)
    //        {
    //            // lower the stance of the player, reducing the collider so the player can go under the table
    //            gameObject.transform.position = vector
    //        }
    //        else
    //        {
    //            // return to original stance
    //        }
    //    }

    //}

    

    void Rotate()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * rotationSpeed;
        currentRotation.y -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Loop the X rotation based on 360 degrees
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);

        // Clamp the Y rotation to avoid flipping the camera upside down
        currentRotation.y = Mathf.Clamp(currentRotation.y, -verticalAngleLimit, verticalAngleLimit);

        // Rotate the camera based on calculated rotation
        Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }

}