using UnityEngine;

public class knightscript : MonoBehaviour
{
    Animator animator;
    
    int IsWalkingHash;
    int IsRunningHash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsRunningHash = Animator.StringToHash("IsRunning");
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(IsRunningHash);
        bool isWalking = animator.GetBool(IsWalkingHash);
        bool forwardPressed = Input.GetKey("g");
        bool runBtn = Input.GetKey("left shift");

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(IsWalkingHash, true);
        }
         

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(IsWalkingHash, false);
        }
        

        if (!isRunning && (forwardPressed && runBtn))
        {
            animator.SetBool(IsRunningHash, true);
        }
         
        if (isRunning && (!forwardPressed || !runBtn))
        {
           animator.SetBool(IsRunningHash, false);  
        }
         
    }
}
