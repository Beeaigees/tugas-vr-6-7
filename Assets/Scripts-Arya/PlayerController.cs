using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // drag Main Camera ke sini

    [Header("Footstep Sound")]
    public AudioClip footstepSound;
    private AudioSource footstepSource;

    Animator animator;

    int IsWalkingHash;
    int IsRunningHash;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {

        footstepSource = gameObject.GetComponent<AudioSource>();
        if (footstepSource == null)
            footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.clip = footstepSound;
        footstepSource.loop = true;
        footstepSource.playOnAwake = false;
        footstepSource.spatialBlend = 0f;
        controller = GetComponent<CharacterController>();

        // Lock & sembunyikan cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component tidak ditemukan di " + gameObject.name);
        }
        else
        {
            Debug.Log("Animator found di: " + animator.gameObject.name);
        }

        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsRunningHash = Animator.StringToHash("IsRunning");
    }

    void Update()
    {
        HandleMovement();
        HandleLook();

        if (animator == null) return;

        bool isRunning = animator.GetBool(IsRunningHash);
        bool isWalking = animator.GetBool(IsWalkingHash);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isMoving = (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);
        // use GetKey for function that needed to be hold like running
        bool runBtn = Input.GetKey(KeyCode.LeftShift);

        if (!isWalking && isMoving)
        {
            animator.SetBool(IsWalkingHash, true);
        }

        if (isWalking && !isMoving)
        {
            animator.SetBool(IsWalkingHash, false);
        }

        if (!isRunning && (isMoving && runBtn))
        {
            animator.SetBool(IsRunningHash, true);
        }

        if (isRunning && (!isMoving || !runBtn))
        {
            animator.SetBool(IsRunningHash, false);
        }
    }

    void HandleMovement()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal"); // A D
        float z = Input.GetAxis("Vertical");   // W S

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? 5f : 3f;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    
        bool isMoving = (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f) && controller.isGrounded;

        if (isMoving)
        {
            footstepSource.pitch = isRunning ? 1.5f : 1f;
            if (!footstepSource.isPlaying)
                footstepSource.Play();
        }
        else
        {
            if (footstepSource.isPlaying)
                footstepSource.Stop();
        }
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}