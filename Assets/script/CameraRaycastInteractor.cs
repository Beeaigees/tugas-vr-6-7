using TMPro;
using UnityEngine;

public class CameraRaycastInteractor : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private Transform cam;
    [SerializeField] private float dist = 4f;
    [SerializeField] private LayerMask mask;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statusTxt;

    private RaycastTargetObject current;
    private RaycastTargetObjectberkalikali currentToggle;

    void Awake()
    {
        if (cam == null && Camera.main != null)
            cam = Camera.main.transform;
    }

    void Update()
    {
        RaycastCheck();

        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    void RaycastCheck()
    {
        if (cam == null)
        {
            Clear();
            return;
        }

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, dist, mask))
        {
            Debug.DrawRay(cam.position, cam.forward * dist, Color.green);
            Debug.Log("Hit : " + hit.collider.name);

            RaycastTargetObject target =
                hit.collider.GetComponent<RaycastTargetObject>();

            if (target == null)
                target = hit.collider.GetComponentInParent<RaycastTargetObject>();

            if (target == null)
                target = hit.collider.GetComponentInChildren<RaycastTargetObject>();

            if (target != null)
            {
                if (current != target)
                {
                    Clear();
                    current = target;
                    current.SetLookedAt(true);
                    Show(current.GetCurrentLabel());
                }
                return;
            }

            RaycastTargetObjectberkalikali toggle =
                hit.collider.GetComponent<RaycastTargetObjectberkalikali>();

            if (toggle == null)
                toggle = hit.collider.GetComponentInParent<RaycastTargetObjectberkalikali>();

            if (toggle != null)
            {
                if (currentToggle != toggle)
                {
                    Clear();
                    currentToggle = toggle;
                    currentToggle.SetLookedAt(true);
                    Show(currentToggle.GetCurrentLabel());
                }
                return;
            }
        }
        else
        {
            Debug.DrawRay(cam.position, cam.forward * dist, Color.red);
        }

        Clear();
    }

    void Interact()
    {
        if (currentToggle != null)
        {
            currentToggle.Interact();
            return;
        }

        if (current != null)
        {
            Debug.Log("Interact dengan : " + current.name);
            current.Interact();
        }
    }

    void Clear()
    {
        if (current != null)
        {
            current.SetLookedAt(false);
            current = null;
        }

        if (currentToggle != null)
        {
            currentToggle.SetLookedAt(false);
            currentToggle = null;
        }

        Show("");
    }

    void Show(string text)
    {
        if (statusTxt != null)
            statusTxt.text = text;
    }
}