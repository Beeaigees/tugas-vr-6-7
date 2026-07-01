using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Reference")]
    public PlayerInventory inv;

    [Header("UI")]
    public TMP_Text slot1Text;
    public TMP_Text slot2Text;
    public TMP_Text slot3Text;
    public TMP_Text holdingText;

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        slot1Text.text = GetSlot(0);
        slot2Text.text = GetSlot(1);
        slot3Text.text = GetSlot(2);

        string current = inv.GetCurrentItem();

        if (string.IsNullOrEmpty(current))
            holdingText.text = "Holding : Empty";
        else
            holdingText.text = "Holding : " + current;
    }

    string GetSlot(int index)
    {
        if (string.IsNullOrEmpty(inv.slots[index]))
            return "---";

        return inv.slots[index];
    }
}