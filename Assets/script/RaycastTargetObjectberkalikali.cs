using UnityEngine;
using UnityEngine.Events;

public class RaycastTargetObjectberkalikali : MonoBehaviour
{
    [Header("Label")]
    public string interactLabel = "[E] Interact";
    public string lockedLabel = "Locked";

    [Header("Canvas Label")]
    [SerializeField] private ProximityCanvasShow _labelCanvas;

    [Header("Info Canvas (yang muncul saat di-E)")]
    [SerializeField] private ProximityCanvasShow _infoCanvas;

    [Header("Events")]
    public UnityEvent onInteract;

    private Renderer _renderer;
    private Color _originalColor;
    private bool _isLocked;
    private bool _isInfoShowing;

    void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer != null)
            _originalColor = _renderer.material.color;
    }

    public void SetLookedAt(bool isLooking)
    {
        if (_isLocked) return;
        if (_renderer == null) return;

        // Highlight kuning saat diliat
        _renderer.material.color = isLooking
            ? Color.yellow
            : _originalColor;

        // Canvas label "[E] Examine Painting"
        // muncul saat diliat, hilang saat gak diliat
        if (_labelCanvas != null)
        {
            if (isLooking) _labelCanvas.Show();
            else _labelCanvas.Hide();
        }
    }

    public void Interact()
    {
        if (_isLocked) return;

        // Toggle info canvas
        _isInfoShowing = !_isInfoShowing;

        if (_infoCanvas != null)
        {
            if (_isInfoShowing) _infoCanvas.Show();
            else _infoCanvas.Hide();
        }

        // Warna hijau saat info terbuka
        // kembali ke original saat info ditutup
        if (_renderer != null)
            _renderer.material.color = _isInfoShowing
                ? Color.green
                : _originalColor;

        onInteract?.Invoke();
        Debug.Log(gameObject.name + " interacted!");
    }

    public void SetLocked(bool locked)
    {
        _isLocked = locked;
        if (_renderer != null)
            _renderer.material.color = locked
                ? Color.red
                : _originalColor;
    }

    public bool IsLocked() => _isLocked;

    public string GetCurrentLabel()
    {
        if (_isLocked) return lockedLabel;
        return interactLabel;
    }
}