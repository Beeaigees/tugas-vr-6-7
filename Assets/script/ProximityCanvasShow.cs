using UnityEngine;

public class ProximityCanvasShow : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Transform _player;
    [SerializeField] private float _showRadius = 3f;

    void Start()
    {
        // Auto cari player kalau belum diassign
        if (_player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                _player = p.transform;
                Debug.Log("Player found: " + p.name);
            }
            else
            {
                Debug.LogError("PLAYER NOT FOUND! Cek tag Player!");
            }
        }

        if (_canvas != null)
            _canvas.SetActive(false);
        else
            Debug.LogError("Canvas belum diassign di " + gameObject.name);
    }

    void Update()
    {
        if (_player == null || _canvas == null) return;

        float dist = Vector3.Distance(
            transform.position,
            _player.position
        );

        // Debug jarak
        Debug.Log(gameObject.name + " jarak ke player: " + dist 
            + " | radius: " + _showRadius);

        _canvas.SetActive(dist <= _showRadius);
    }

    // Fungsi Show/Hide tetap ada buat lukisan
    public void Show()
    {
        if (_canvas != null)
            _canvas.SetActive(true);
    }

    public void Hide()
    {
        if (_canvas != null)
            _canvas.SetActive(false);
    }
}