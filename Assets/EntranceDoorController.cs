using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EntranceDoorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject promptCanvas;
    [SerializeField] private Transform playerCamera;
    // [SerializeField] private GameObject handcard;

    [Header("Prompt UI (worldspace)")]
    [SerializeField] private GameObject promptText;
    [SerializeField] private GameObject box;
    [SerializeField] private Image progressFillImage;

    // [Header("Feedback")]
    // [SerializeField] private GameObject feedbackMessage;
    // [SerializeField] private float feedbackDuration = 2f;

    [Header("Settings")]
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    [SerializeField] private float holdDuration = 1.5f;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string openTriggerName = "Open";

    [SerializeField] private SlidingDoor slidingDoor;

    // private Coroutine feedbackCoroutine;

    private bool playerInRange = false;
    private bool isHolding = false;
    private float holdTimer = 0f;
    private bool isOpen = false;

    void Start()
    {
        if (promptCanvas != null)
            promptCanvas.SetActive(false);

        if (progressFillImage != null)
            progressFillImage.fillAmount = 0f;

        // feedbackMessage.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange || isOpen) return;

        if (playerCamera != null && promptCanvas != null)
            promptCanvas.transform.LookAt(2 * promptCanvas.transform.position - playerCamera.position);

        // bool hasKeycard = handcard != null && handcard.activeSelf;

        // if (!hasKeycard)
        // {
        //     if (Input.GetKeyDown(interactKey))
        //         ShowFeedback();

        //     ResetHold();
        //     return;
        // }

        if (Input.GetKeyDown(interactKey))
            StartHold();

        if (Input.GetKey(interactKey) && isHolding)
        {
            holdTimer += Time.deltaTime;

            if (progressFillImage != null)
                progressFillImage.fillAmount = holdTimer / holdDuration;

            if (holdTimer >= holdDuration)
                OpenDoor();
        }

        if (Input.GetKeyUp(interactKey))
            ResetHold();
    }

    private void StartHold()
    {
        isHolding = true;
        holdTimer = 0f;

        if (box != null)
            box.SetActive(false);
    }

    private void ResetHold()
    {
        isHolding = false;
        holdTimer = 0f;

        if (progressFillImage != null)
            progressFillImage.fillAmount = 0f;

        if (box != null)
            box.SetActive(true);
    }

    private void OpenDoor()
    {
        isOpen = true;
        ResetHold();

        if (promptCanvas != null)
            promptCanvas.SetActive(false);

        Debug.Log("Door opened");

        if (slidingDoor != null)
            slidingDoor.Open();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (!isOpen && promptCanvas != null)
            promptCanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (promptCanvas != null)
            promptCanvas.SetActive(false);

        // HideFeedback();
        ResetHold();
    }

    // private void ShowFeedback()
    // {
    //     if (feedbackMessage == null) return;

    //     if (feedbackCoroutine != null)
    //         StopCoroutine(feedbackCoroutine);

    //     feedbackMessage.SetActive(true);
    //     feedbackCoroutine = StartCoroutine(HideFeedbackAfterDelay());
    // }

    // private void HideFeedback()
    // {
    //     if (feedbackCoroutine != null)
    //     {
    //         StopCoroutine(feedbackCoroutine);
    //         feedbackCoroutine = null;
    //     }

    //     if (feedbackMessage != null)
    //         feedbackMessage.SetActive(false);
    // }

    // private IEnumerator HideFeedbackAfterDelay()
    // {
    //     yield return new WaitForSeconds(feedbackDuration);
    //     feedbackMessage.SetActive(false);
    //     feedbackCoroutine = null;
    // }
}