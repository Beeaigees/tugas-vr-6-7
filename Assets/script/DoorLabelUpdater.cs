using TMPro;
using UnityEngine;

public class DoorLabelUpdater : MonoBehaviour
{
    [SerializeField] private GameObject _labelCanvas;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private ExitDoor _exitDoor;
    [SerializeField] private Transform _player;
    [SerializeField] private float _showRadius = 3f;

    void Start()
    {
        if (_player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) _player = p.transform;
        }
        if (_labelCanvas != null)
            _labelCanvas.SetActive(false);
    }

    void Update()
    {
        if (_player == null || _labelCanvas == null) return;

        float dist = Vector3.Distance(
            transform.position,
            _player.position
        );

        if (dist <= _showRadius)
        {
            _labelCanvas.SetActive(true);

            // Update text sesuai kondisi kunci
            if (_labelText != null && _exitDoor != null)
                _labelText.text = _exitDoor.GetLabel();
        }
        else
        {
            _labelCanvas.SetActive(false);
        }
    }
}