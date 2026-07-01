using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // drag Main Camera ke sini

    Animator animator;

    int IsWalkingHash;
    int IsRunningHash;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
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

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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