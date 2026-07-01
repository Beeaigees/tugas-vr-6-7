using UnityEngine;

public class ActionFigure : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip pickupSound; 
    public AudioClip interactSound;

    [Header("Settings")]
    public float interactRange = 2f;

    [Header("Inventory")]
    public Sprite inventoryIcon;

    [Header("Hold Offset")]
    public Vector3 holdpositionOffset;
    public Vector3 holdrotationOffset;
    private AudioSource audioSource;
    private bool isHeld = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    public void OnPickup()
    {
        isHeld = true;
        if (pickupSound != null)
            audioSource.PlayOneShot(pickupSound, 0.5f);
    }

    public void OnInteract()
    {
        if(!isHeld) return;

        if (interactSound != null)
        audioSource.PlayOneShot(interactSound, 0.5f);
    }

    public void OnDrop()
    {
        isHeld = false;
    }

    public bool IsHeld => isHeld;
}
