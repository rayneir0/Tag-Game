using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 3f;       // Walking speed
    public float turnSpeed = 2f;      // How fast to face the movement direction
    // public float stopSmoothness = 15f; // How quickly we stop sliding when input is released
    public Transform cameraTransform;
    public float mouseSensitivity = 1f; // tweak for speed
    private Rigidbody rb; 
    private Animator animator;
    private Vector3 inputDirection;
    private Vector3 moveDirection;
    private float rotationX = 0f;   // vertical rotation
    private float rotationY = 0f;   // horizontal rotation
    private Quaternion targetRotation; 
    private bool hasRotationUpdate = false;

    // Pause Game
    public PauseMenuController pauseMenu;


    void Start()
    {

        if (TagManager.Instance.currentIt == null) // Sets the player as it at the start by default
            TagManager.Instance.SetIt(gameObject);

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Locking the screen so the cursor doesn't move
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Auto-assign camera if not set
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {   
        // Getting the player inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Animation control
        bool isWalking = inputDirection.magnitude > 0.1f;
        animator.SetBool("IsWalking", isWalking);

        // Calculate rotation 
        CalculateRotation();
    }

    // Handle physics-based movement in FixedUpdate
    void FixedUpdate()
    {
        // Apply rotation in FixedUpdate for physics consistency
        if (hasRotationUpdate)
        {
            rb.MoveRotation(targetRotation);
            hasRotationUpdate = false;
        }

        // Move camera with player
        if (inputDirection.magnitude > 0f)
        {
            Vector3 camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 camRight = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;
            moveDirection = (camForward * inputDirection.z + camRight * inputDirection.x).normalized;

            // Move character
            Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }


    }

    // Calculate rotation based on mouse and apply this to the camera
    void CalculateRotation()
    {
        if (pauseMenu != null && pauseMenu.GetPausedState())
            return;
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Apply sensitivity
        rotationY += mouseX * mouseSensitivity;
        rotationX -= mouseY * mouseSensitivity;

        rotationX = Mathf.Clamp(rotationX, -25f, 60f);

        // Store target rotation for FixedUpdate
        targetRotation = Quaternion.Euler(0f, rotationY, 0f);
        hasRotationUpdate = true;
     

        rb.MoveRotation(targetRotation);
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }

}

