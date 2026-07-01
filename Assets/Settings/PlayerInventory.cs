using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public string[] slots = new string[3];

    [Header("Current Selected Slot")]
    public int currentSlot = 0;

    // TARUH DI SINI
    private void Start()
    {
        slots[0] = "Key";
        slots[1] = "Potion";
    }

    private void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb == null) return;

        if (kb.digit1Key.wasPressedThisFrame)
            currentSlot = 0;

        if (kb.digit2Key.wasPressedThisFrame)
            currentSlot = 1;

        if (kb.digit3Key.wasPressedThisFrame)
            currentSlot = 2;
    }

    public bool AddItem(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (string.IsNullOrEmpty(slots[i]))
            {
                slots[i] = itemName;
                return true;
            }
        }

        return false;
    }

    public string GetCurrentItem()
    {
        if (string.IsNullOrEmpty(slots[currentSlot]))
            return "";

        return slots[currentSlot];
    }
}