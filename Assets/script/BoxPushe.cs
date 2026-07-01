using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxPusher : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private LayerMask pushableLayer;

    [Header("Force — tuning di sini")]
    [Tooltip("Kekuatan dorong. Mulai dari 5-15 untuk feel Suikoden")]
    [SerializeField] private float pushForce = 8f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private float feedbackDuration = 1.2f;

    // Cooldown — cegah push berkali-kali dalam satu klik
    private bool _canPush = true;
    private float _cooldown = 0.3f;
    private float _cooldownTimer = 0f;
    private float _feedbackTimer = 0f;
    private bool _showFeedback = false;

    void Awake()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleCooldown();
        HandleLookLabel();
        HandlePush();
        HandleFeedback();
    }

    // === COOLDOWN — jeda 0.3 detik antar push ===
    void HandleCooldown()
    {
        if (_canPush) return;
        _cooldownTimer -= Time.deltaTime;
        if (_cooldownTimer <= 0f)
            _canPush = true;
    }

    // === LABEL "Left Click to Push" saat melihat box ===
    void HandleLookLabel()
    {
        if (_showFeedback) return;
        bool hit = Physics.Raycast(
            cameraTransform.position,
            cameraTransform.forward,
            maxDistance,
            pushableLayer
        );
        Show(hit ? "🖱 Left Click to Push" : "");
    }

    // === PUSH — inti logika dorong ===
    void HandlePush()
    {
        // Cek klik kiri — hanya sekali per frame
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        // Cek cooldown
        if (!_canPush) return;

        // Raycast dari kamera
        if (!Physics.Raycast(cameraTransform.position, cameraTransform.forward,
            out RaycastHit hit, maxDistance, pushableLayer)) return;

        Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
        if (rb == null) return;

        // === FIX UTAMA ===
        // Arah dorong = HORIZONTAL ONLY dari kamera
        // Ignore sumbu Y supaya box tidak mental ke atas
        Vector3 pushDir = cameraTransform.forward;
        pushDir.y = 0f;         // paksa Y = 0, horizontal murni
        pushDir.Normalize();     // normalize ulang setelah Y di-zero

        // Gunakan VelocityChange bukan Impulse
        // VelocityChange = langsung set kecepatan, tidak terpengaruh mass
        // Hasilnya: push terasa konsisten, bukan "mental" kayak Impulse
        rb.AddForce(pushDir * pushForce, ForceMode.VelocityChange);

        // Aktifkan cooldown
        _canPush = false;
        _cooldownTimer = _cooldown;

        ShowFeedback("Pushing...");
        Debug.Log("Pushed " + hit.collider.name + " | dir: " + pushDir);
    }

    // === FEEDBACK TIMER ===
    void HandleFeedback()
    {
        if (!_showFeedback) return;
        _feedbackTimer -= Time.deltaTime;
        if (_feedbackTimer <= 0f)
        {
            _showFeedback = false;
            Show("");
        }
    }

    void ShowFeedback(string msg)
    {
        _showFeedback = true;
        _feedbackTimer = feedbackDuration;
        Show(msg);
    }

    void Show(string s)
    {
        if (statusText != null) statusText.text = s;
    }
}