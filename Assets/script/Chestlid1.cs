using UnityEngine;
using System.Collections;

public class ChestLid : MonoBehaviour
{
    [SerializeField] private Transform _lidPivot;
    [SerializeField] private float _openAngleX = -66f;
    [SerializeField] private float _openSpeed = 2f;
    [SerializeField] private GameObject _openedLabel;

    [SerializeField] public GameObject _actionFigure;

    private bool _isOpen;
    private Quaternion _closedRot;
    private Quaternion _openRot;

    void Awake()
    {
        if (_lidPivot == null) return;
        _closedRot = _lidPivot.localRotation;
        _openRot = Quaternion.Euler(_openAngleX, 0f, 0f) * _closedRot;
        _actionFigure.SetActive(false);
    }

    public void ToggleChest()
    {
        if (_isOpen) return;
        _isOpen = true;
        _actionFigure.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RotateLid(_openRot));
        if (_openedLabel != null) _openedLabel.SetActive(true);
    }

    private IEnumerator RotateLid(Quaternion target)
    {
        while (Quaternion.Angle(_lidPivot.localRotation, target) > 0.5f)
        {
            _lidPivot.localRotation = Quaternion.Slerp(_lidPivot.localRotation, target, Time.deltaTime * _openSpeed);
            yield return null;
        }
        _lidPivot.localRotation = target;
    }
}