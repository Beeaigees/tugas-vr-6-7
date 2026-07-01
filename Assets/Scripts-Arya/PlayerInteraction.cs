using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float pickupRange = 2.5f;
    public Transform holdPosition; // titik di tangan player tempat item ditahan
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode interactKey = KeyCode.F;

    [Header("UI (optional)")]
    public GameObject interactPromptUI; // UI "Press E to pick up"

    private ActionFigure heldFigure = null;
    private ActionFigure nearbyFigure = null;

    void Update()
    {
        DetectNearby();
        HandleInput();
    }

    void DetectNearby()
    {
        // Raycast atau OverlapSphere untuk detect action figure terdekat
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange);
        nearbyFigure = null;

        foreach (var hit in hits)
        {
            var fig = hit.GetComponent<ActionFigure>();
            // ini dibaca apakah fig ini ada? dan apakah fig ini belum dipegang?, mereka mengreturn true karna fig ini ada dan fig ini blom dipegang
            if (fig != null && !fig.IsHeld) 
            {
                nearbyFigure = fig;
                break;
            }
        }

        // Tampilkan/sembunyikan prompt UI
        if (interactPromptUI != null)
            interactPromptUI.SetActive(nearbyFigure != null && heldFigure == null);
    }

    void HandleInput()
    {
        // Pickup, pakai GetKeyDown buat one time use 
        if (Input.GetKeyDown(pickupKey) && nearbyFigure != null && heldFigure == null)
        {
            PickupFigure(nearbyFigure);
        }

        // Interact saat digenggam
        if (Input.GetKeyDown(interactKey) && heldFigure != null)
        {
            heldFigure.OnInteract();
        }

        // Drop (opsional, misal tekan E lagi)
        if (Input.GetKeyDown(KeyCode.G) && heldFigure != null)
        {
            DropFigure();
        }
    }

    void PickupFigure(ActionFigure figure)
    {
        heldFigure = figure;
        heldFigure.OnPickup();

        // Attach ke tangan player
        figure.transform.SetParent(holdPosition);
        figure.transform.localPosition = Vector3.zero;
        figure.transform.localRotation = Quaternion.identity;

        // Matikan physics sementara
        var rb = figure.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        var col = figure.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    void DropFigure()
    {
        if (heldFigure == null) return;

        heldFigure.OnDrop();
        heldFigure.transform.SetParent(null);

        var rb = heldFigure.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        var col = heldFigure.GetComponent<Collider>();
        if (col != null) col.enabled = true;

        heldFigure = null;
    }
}