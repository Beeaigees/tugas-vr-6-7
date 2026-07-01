using UnityEngine;

public class ZombieTrigger : MonoBehaviour
{
   public Animator zombieAnimator;
   public string animationTrigger = "Activate";

   private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;
        zombieAnimator.SetTrigger(animationTrigger);
    }
}
