using UnityEngine;

public class PaintingInfo : MonoBehaviour
{
    [SerializeField] private GameObject _infoCanvas;
    private bool _isShowing = false;

    // Dipanggil dari OnInteract() RaycastTargetObject
    public void ToggleInfo()
    {
        _isShowing = !_isShowing;
        if (_infoCanvas != null)
            _infoCanvas.SetActive(_isShowing);
    }
}