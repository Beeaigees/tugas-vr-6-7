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

    public InventorySystem inventory;
    
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

        // buat akses item
        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.SwitchItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.SwitchItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.SwitchItem(2);
    }

    void PickupFigure(ActionFigure figure)
    {

        Debug.Log("PickupFigure dipanggil: " + figure.gameObject.name);
        if (!inventory.AddItem(figure)) return;

        heldFigure = figure;
        heldFigure.OnPickup();

        // Attach ke tangan player
        figure.transform.SetParent(holdPosition);
        figure.transform.localPosition = figure.holdpositionOffset;
        figure.transform.localRotation = Quaternion.Euler(figure.holdrotationOffset);

        // Matikan physics sementara
        var rb = figure.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        var col = figure.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    void DropFigure()
    {

        Debug.Log("Drop - activeSlot: " + inventory.activeSlot);
        if (heldFigure == null) return;

        inventory.RemoveItem(inventory.activeSlot);
        heldFigure.gameObject.SetActive(true);
        heldFigure.OnDrop();
        heldFigure.transform.SetParent(null);
        
        var rb = heldFigure.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        var col = heldFigure.GetComponent<Collider>();
        if (col != null) col.enabled = true;

        heldFigure = null;
    }

    public void SetHeldFigure(ActionFigure figure)
    {

        Debug.Log("SetHeldFigure dipanggil, figure: " + (figure != null ? figure.gameObject.name : "null"));
        // Lepas item lama dari tangan dulu
        // Sembunyikan item lama
        if (heldFigure != null)
        {
            heldFigure.gameObject.SetActive(false);
            heldFigure.OnDrop();
        }

        heldFigure = figure;

        // Tampilkan item baru
        if (heldFigure != null)
        {
            heldFigure.gameObject.SetActive(true);
            heldFigure.transform.SetParent(holdPosition);
            heldFigure.transform.localPosition = figure.holdpositionOffset;
            heldFigure.transform.localRotation = Quaternion.Euler(figure.holdrotationOffset);
            heldFigure.OnPickup();
        }
    }
}