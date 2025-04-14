using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class DraculaController : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    public float sprintSpeed = 2.0f;
    public float jumpStrength = 4.0f;
    public float rotationSpeed = 1.0f;
    public float verticalAngleLimit = 85.0f;

    //====Added variable for tracking # of jumps====
    public int jumpCount = 0;
    public Transform groundCheck;
    public float groundDistance = .2f;
    public LayerMask groundMask;
    private bool isGrounded;

    //===Crouching variables===
    private bool isCrouched = false;
    private CapsuleCollider capsuleCollider;
    public float crouchHeight = 0.5f;
    public float standHeight = 2.0f;
    public float checkHeightOffset = 0.9f; //I think this height is good but adjust if needed
    public LayerMask obstacleLayer;


    private Vector3 currentRotation;

    Rigidbody rb;

    // Game Over Manager
    private GameOverMenu gameOverManager;

    // ===== Health variables =====
    private float maxHealth = 100f;
    private float currentHealth = 100f;
    public static float damageNum = 10f;
    private float regenNum = 1f;
    [SerializeField] private bool isBeingDamaged = false;
    private bool isBeingHealed = false;
    public static bool isInLight;
    public static bool isNearLight;
    private bool nearLightIndicatorShowing = false;
    public HealthBar healthBar;

    //==== UI Elements ====
    public Image DamageIndicator;
    public Image NearLightIndicator;

    //==== Sound Effects ====
    private AudioSource walkAudioSource;
    private AudioSource breathAudioSource;
    private AudioSource regenAudioSource;
    private AudioSource heartbeatAudioSource;
    private AudioSource burningAudioSource;

    public AudioClip walkSoundClip;
    public AudioClip breathingAudioClip;
    public AudioClip regenAudioClip;
    public AudioClip heartbeatAudioClip;
    public AudioClip burningAudioClip;

    private bool isMoving = false;

    public AudioClip jumpSoundClip;





    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; // Hide cursor when playing.

        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        capsuleCollider = GetComponent<CapsuleCollider>();

        DamageIndicator = GameObject.Find("DamageIndicator").GetComponent<Image>();
        DamageIndicator.enabled = false;
        NearLightIndicator = GameObject.Find("NearLightIndicator").GetComponent<Image>();
        NearLightIndicator.enabled = false;

        currentRotation = new Vector3(-90, 0, 0);

        gameOverManager = FindFirstObjectByType<GameOverMenu>();

        isInLight = false;

        walkAudioSource = gameObject.AddComponent<AudioSource>();
        breathAudioSource = gameObject.AddComponent<AudioSource>();
        regenAudioSource = gameObject.AddComponent<AudioSource>();
        heartbeatAudioSource = gameObject.AddComponent<AudioSource>();
        burningAudioSource = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            MovePlayer();


            // New Jump functionality
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if(isGrounded)
            {
                jumpCount = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                jumpCount++;
            }

            //Jump();
            Rotate();

            showNearLightIndicator();

            // ==== Crouch Behavior ====
            Crouch();


            // ===== Health behaviors =====
            DamagePlayer();
            RegenPlayer();
            //Debug.Log("Health: " + currentHealth);
        }
    }

    void MovePlayer()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        float y = rb.linearVelocity.y;

        if (Input.GetKey(KeyCode.W)) direction += Camera.main.transform.forward;
        if (Input.GetKey(KeyCode.S)) direction -= Camera.main.transform.forward;
        if (Input.GetKey(KeyCode.A)) direction -= Camera.main.transform.right;
        if (Input.GetKey(KeyCode.D)) direction += Camera.main.transform.right;

        float movementSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        Vector3 velocity = direction.normalized * movementSpeed;
        velocity.y = y;
        rb.linearVelocity = velocity;

        // Handle walking sound
        bool isCurrentlyMoving = direction != Vector3.zero;

        if (isCurrentlyMoving && !isMoving)
        {
            isMoving = true;
            SoundFXManager.instance.PlayLoopingSound(walkSoundClip, walkAudioSource, 0.1f);
            SoundFXManager.instance.PlayLoopingSound(breathingAudioClip, breathAudioSource, 1f);
        }

        // Sprint
        float movementSpeed = 0f;
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouched)
        {
            isMoving = false;
            SoundFXManager.instance.StopLoopingSound(walkAudioSource);
            SoundFXManager.instance.StopLoopingSound(breathAudioSource);
        }
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

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player is landing on the ground (adjust the tag as needed)
        if (collision.gameObject.CompareTag("Ground") && !isCrouched)
        {
            // Play landing sound when the player hits the ground
            SoundFXManager.instance.PlaySoundFXClip(jumpSoundClip, 0.6f);
            jumpCount = 0; // Reset jump count to allow the next jump
        }
    }


    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouched)
            {
                //This will only allow the player stand up if there's no obstacle detected
                if (!Physics.Raycast(transform.position, Vector3.up, checkHeightOffset, obstacleLayer))
                {
                    isCrouched = false;
                    capsuleCollider.height = standHeight;
                }
            }
            else
            {
                //the code for crouching down
                isCrouched = true;
                capsuleCollider.height = crouchHeight;
            }
        }
    }



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


    void showNearLightIndicator()
    {
        // Shows indicator that player is near light
        if (isNearLight && !nearLightIndicatorShowing)
        {
            StartCoroutine(FadeInNearLightIndicator());
            SoundFXManager.instance.PlayLoopingSound(heartbeatAudioClip, heartbeatAudioSource, 1f);

            nearLightIndicatorShowing = true;
        }
        // Gets rid of indicator showing that player is near light
        else if (!isNearLight && nearLightIndicatorShowing)
        {
            StartCoroutine(FadeOutNearLightIndicator());
            SoundFXManager.instance.StopLoopingSound(heartbeatAudioSource);

            nearLightIndicatorShowing = false;
        }
    }


    // ===== Health functions =====
    public static void setDamageNum(float damage)
    {
        damageNum = damage;
    }

    void DamageHealth()
    {
        currentHealth -= damageNum;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameOverManager.GameOver();
        }
    }

    public void ResetPlayer()
    {
        currentHealth = maxHealth;
        isBeingDamaged = false;
        isInLight = false;
        CancelInvoke("DamageHealth");
        healthBar.SetHealth(currentHealth);
        DamageIndicator.enabled = false;
    }

    public void Die()
    {
        currentHealth = 0;
        healthBar.SetHealth(currentHealth);
        gameOverManager.GameOver();
    }

    void DamagePlayer()
    {
        // Damage player if in light
        if (isInLight && !isBeingDamaged)
        {
            isBeingDamaged = true;
            InvokeRepeating("DamageHealth", 0f, 1f);
            StartCoroutine(FadeInDamageIndicator());
            SoundFXManager.instance.PlayLoopingSound(heartbeatAudioClip, heartbeatAudioSource, 1f);
            SoundFXManager.instance.PlayLoopingSound(burningAudioClip, burningAudioSource, 0.2f);
        }
        // Stop damaging player if not in light or health is 0
        else if ((!isInLight && isBeingDamaged) || currentHealth <= 0)
        {
            CancelInvoke("DamageHealth");
            StartCoroutine(FadeOutDamageIndicator());
            SoundFXManager.instance.StopLoopingSound(heartbeatAudioSource);
            SoundFXManager.instance.StopLoopingSound(burningAudioSource);
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
            SoundFXManager.instance.PlayLoopingSound(regenAudioClip, regenAudioSource, 1f);
        }
        // Stop regen if max health is reached or taking damage
        else if (currentHealth == maxHealth || isBeingDamaged)
        {
            isBeingHealed = false;
            CancelInvoke("RegenHealth");
            SoundFXManager.instance.StopLoopingSound(regenAudioSource);
        }
    }

    IEnumerator FadeInDamageIndicator()
    {
        DamageIndicator.enabled = true; // Making it visible
        Color color = DamageIndicator.color;
        float duration = 0.5f; // Time to fully fade in
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            DamageIndicator.color = color;
            yield return null;
        }
        color.a = 1f;
        DamageIndicator.color = color;
    }

    IEnumerator FadeOutDamageIndicator()
    {
        Color color = DamageIndicator.color;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            DamageIndicator.color = color;
            yield return null;
        }
        color.a = 0f;
        DamageIndicator.color = color;
        DamageIndicator.enabled = false; // Fading it back to transparent
    }

    IEnumerator FadeInNearLightIndicator()
    {
        NearLightIndicator.enabled = true; // Making it visible
        Color color = NearLightIndicator.color;
        float duration = 0.5f; // Time to fully fade in
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            NearLightIndicator.color = color;
            yield return null;
        }
        color.a = 1f;
        NearLightIndicator.color = color;
    }

    IEnumerator FadeOutNearLightIndicator()
    {
        Color color = NearLightIndicator.color;
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            NearLightIndicator.color = color;
            yield return null;
        }
        color.a = 0f;
        NearLightIndicator.color = color;
        NearLightIndicator.enabled = false; // Fading it back to transparent
    }
}

