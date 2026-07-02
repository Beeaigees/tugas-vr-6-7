using UnityEngine;
using UnityEngine.Events;

public class RaycastTargetObject : MonoBehaviour
{
    public string interactLabel = "[B] Interact";
    public string lockedLabel = "Locked";
    public UnityEvent onInteract;

    private Renderer _renderer;
    private Color _originalColor;
    private bool _isLocked;
    private bool _isInteracted;

    void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer != null)
            _originalColor = _renderer.material.color;
    }

    public void SetLookedAt(bool isLooking)
    {
        if (_isInteracted || _isLocked) return;
        if (_renderer == null) return;
        _renderer.material.color = isLooking ? Color.yellow : _originalColor;
    }

    public string GetCurrentLabel()
    {
        if (_isLocked) return lockedLabel;
        if (_isInteracted) return "";
        return interactLabel;
    }

    public void Interact()
    {
        if (_isLocked || _isInteracted) return;
        _isInteracted = true;
        if (_renderer != null)
            _renderer.material.color = Color.green;
        onInteract?.Invoke();
        Debug.Log(gameObject.name + " interacted!");
    }

    public void SetLocked(bool locked)
    {
        _isLocked = locked;
        if (_renderer != null)
            _renderer.material.color = locked ? Color.red : _originalColor;
    }

    public bool IsLocked() => _isLocked;
}