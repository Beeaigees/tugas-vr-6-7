using UnityEngine;

public class ActionFigure : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip pickupSound; 
    public AudioClip interactSound;

    [Header("Settings")]
    public float interactRange = 2f;

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
            audioSource.PlayOneShot(pickupSound);
    }

    public void OnInteract()
    {
        if(!isHeld) return;

        if (interactSound != null)
        audioSource.PlayOneShot(interactSound);
    }

    public void OnDrop()
    {
        isHeld = false;
    }

    public bool IsHeld => isHeld;
}
