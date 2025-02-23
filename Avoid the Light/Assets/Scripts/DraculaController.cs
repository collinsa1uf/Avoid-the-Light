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


    // ===== Health variables =====
    private float maxHealth = 100f;
    private float currentHealth = 100f;
    private float damageNum = 10f;
    private float regenNum = 1f;
    private bool isBeingDamaged = false;
    private bool isBeingHealed = false;
    public static bool isInLight = false;
    public HealthBar healthBar;


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


        // ===== Health behaviors =====
        DamagePlayer();
        RegenPlayer();
        Debug.Log("Health: " + currentHealth);
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


    // ===== Health functions =====
    void DamageHealth()
    {
        currentHealth -= damageNum;
        healthBar.SetHealth(currentHealth);
    }

    void DamagePlayer()
    {
        // Damage player if in light
        if (isInLight && !isBeingDamaged)
        {
            isBeingDamaged = true;
            InvokeRepeating("DamageHealth", 0f, 1f);
        }
        // Stop damaging player if not in light or health is 0
        else if ((!isInLight && isBeingDamaged) || currentHealth <= 0)
        {
            CancelInvoke("DamageHealth");
        }
    }

    void RegenHealth()
    {
        currentHealth += regenNum;
        healthBar.SetHealth(currentHealth);
    }

    void RegenPlayer()
    {
        // Regen if not in light and health is not at max
        if (!isInLight && !isBeingHealed && currentHealth < 100f)
        {
            isBeingDamaged = false;
            isBeingHealed = true;
            InvokeRepeating("RegenHealth", 0f, 1f);
        }
        // Stop regen if max health is reached or taking damage
        else if (currentHealth == maxHealth || isBeingDamaged)
        {
            isBeingHealed = false;
            CancelInvoke("RegenHealth");
        }
    }
}