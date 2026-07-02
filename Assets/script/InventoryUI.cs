using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Reference")]
    public PlayerInventory inv;

    [Header("Slot Texts")]
    public TMP_Text slot1Text;
    public TMP_Text slot2Text;
    public TMP_Text slot3Text;
    public TMP_Text holdingText;

    // Warna slot
    private Color activeColor = Color.yellow;
    private Color filledColor = Color.white;
    private Color emptyColor = new Color(1, 1, 1, 0.3f);

    void Update()
    {
        if (inv == null) return;

        UpdateUI();
    }

    void UpdateUI()
    {
        UpdateSlot(slot1Text, 0);
        UpdateSlot(slot2Text, 1);
        UpdateSlot(slot3Text, 2);
        UpdateHolding();
    }

    void UpdateSlot(TMP_Text txt, int index)
    {
        if (txt == null) return;

        // GANTI activeSlot -> currentSlot
        bool isActive = (inv.currentSlot == index);

        string item = inv.slots[index];
        bool isEmpty = string.IsNullOrEmpty(item);

        // Format teks slot
        string prefix = "[" + (index + 1) + "] ";
        txt.text = prefix + (isEmpty ? "---" : GetDisplayName(item));

        // Warna slot
        if (isActive)
        {
            txt.color = activeColor;
            txt.fontStyle = FontStyles.Bold;
        }
        else if (isEmpty)
        {
            txt.color = emptyColor;
            txt.fontStyle = FontStyles.Normal;
        }
        else
        {
            txt.color = filledColor;
            txt.fontStyle = FontStyles.Normal;
        }
    }

    void UpdateHolding()
    {
        if (holdingText == null) return;

        string current = inv.GetCurrentItem();

        if (string.IsNullOrEmpty(current))
            holdingText.text = "Holding: Empty";
        else
            holdingText.text = "Holding: " + GetDisplayName(current);
    }

    string GetDisplayName(string id)
    {
        switch (id)
        {
            case "exit_key":
                return "Exit Key";

            case "treasure":
                return "Treasure Key";

            case "build":
                return "KR Build Figure";

            case "exaid":
                return "Gashat Ex-Aid";

            default:
                return id;
        }
    }
}