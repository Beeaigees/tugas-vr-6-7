using TMPro;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [Header("Key Setting")]
    [SerializeField] private string requiredKeyID = "exit_key";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private float feedbackDuration = 2f;

    [Header("References")]
    [SerializeField] private KeyHolder playerKeyHolder;

    private bool _isOpen = false;
    private float _feedbackTimer = 0f;
    private bool _showingFeedback = false;

    void Update()
    {
        // Timer feedback
        if (_showingFeedback)
        {
            _feedbackTimer -= Time.deltaTime;
            if (_feedbackTimer <= 0f)
            {
                _showingFeedback = false;
                if (statusText != null)
                    statusText.text = "";
            }
        }
    }

    // Dipanggil dari RaycastTargetObject OnInteract()
    public void TryOpen()
    {
        if (_isOpen) return;

        // Cek player pegang kunci gak
        if (playerKeyHolder == null)
        {
            // Auto cari KeyHolder di scene
            playerKeyHolder = Object.FindAnyObjectByType<KeyHolder>();
        }

        if (playerKeyHolder == null)
        {
            ShowFeedback("❌ Error: No player found!");
            return;
        }

        // Gak punya kunci sama sekali
        if (playerKeyHolder.heldKey == "")
        {
            ShowFeedback("🔒 Need Exit Key!");
            return;
        }

        // Punya kunci tapi salah
        if (!playerKeyHolder.HasKey(requiredKeyID))
        {
            ShowFeedback("❌ Wrong Key!");
            return;
        }

        // Kunci cocok — buka pintu
        _isOpen = true;
        playerKeyHolder.RemoveKey();
        ShowFeedback("✅ Exit Opened!");
        OpenDoor();
    }

    // Label saat player lihat pintu
    public string GetLabel()
    {
        if (_isOpen) return "Door is open";

        if (playerKeyHolder == null)
            playerKeyHolder = Object.FindAnyObjectByType<KeyHolder>();

        if (playerKeyHolder == null) return "[E] Open Exit";

        if (playerKeyHolder.heldKey == "")
            return "🔒 Need Exit Key";

        if (!playerKeyHolder.HasKey(requiredKeyID))
            return "❌ Wrong Key";

        return "[E] Open Exit";
    }

    void OpenDoor()
    {
        // Matiin collider pintu biar bisa dilewati
        var col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // Sembunyiin pintu
        var rend = GetComponent<Renderer>();
        if (rend != null) rend.enabled = false;

        Debug.Log("Exit Door opened!");
    }

    void ShowFeedback(string msg)
    {
        if (statusText != null)
            statusText.text = msg;
        _showingFeedback = true;
        _feedbackTimer = feedbackDuration;
    }
}