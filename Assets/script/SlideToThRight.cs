using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform doorTransform;

    [Header("Settings")]
    [SerializeField] private float slideDistance = 1.5f;
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private Vector3 slideDirection = Vector3.right;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Coroutine slideRoutine;

    void Awake()
    {
        if (doorTransform == null)
            doorTransform = transform;

        closedPosition = doorTransform.localPosition;
        openPosition = closedPosition + slideDirection.normalized * slideDistance;
    }

    public void Open()
    {
        if (slideRoutine != null)
            StopCoroutine(slideRoutine);

        slideRoutine = StartCoroutine(SlideTo(openPosition));
    }
    public void Close()
    {
        if (slideRoutine != null)
            StopCoroutine(slideRoutine);

        slideRoutine = StartCoroutine(SlideTo(closedPosition));
    }

    private IEnumerator SlideTo(Vector3 target)
    {
        Vector3 start = doorTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / slideDuration);
            t = t * t * (3f - 2f * t);

            doorTransform.localPosition = Vector3.Lerp(start, target, t);
            yield return null;
        }

        doorTransform.localPosition = target;
    }
}