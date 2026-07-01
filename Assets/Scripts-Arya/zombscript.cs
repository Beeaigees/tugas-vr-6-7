using UnityEngine;

public class zombscript : MonoBehaviour
{

    Animator animator;
    int IsHeadbuttHash;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        IsHeadbuttHash = Animator.StringToHash("IsHeadbutt");
    }

    // Update is called once per frame
    void Update()
    {
        bool isHeadbutting = animator.GetBool("IsHeadButtHash");
        bool btnActivation = Input.GetKey("r");
        
        if (!isHeadbutting && btnActivation)
            animator.SetBool("IsHeadButtHash", true);

        if (isHeadbutting && !btnActivation)
            animator.SetBool("IsHeadButtHash", false);
    }
}
