using UnityEngine;

public class closed : MonoBehaviour
{
    [SerializeField] private SlidingDoor slidingDoor;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isTriggered) return;

        isTriggered = true;
        if (slidingDoor != null)
            slidingDoor.Close();
    }
}