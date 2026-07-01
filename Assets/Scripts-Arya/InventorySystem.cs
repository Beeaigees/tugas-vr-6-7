
using UnityEngine;


public class InventorySystem : MonoBehaviour
{
    [Header("UI Slots")]
    public UnityEngine.UI.Image [] slotIcons; // 4 slot icon di HUD
    
    [Header("Settings")]
    public int activeSlot = 0;

    private ActionFigure[] slots = new ActionFigure[3];
    
    public PlayerInteraction playerInteraction;
    public bool AddItem(ActionFigure figure)
    {   
        Debug.Log("AddItem dipanggil: " + figure.gameObject.name);
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = figure;
                slotIcons[i].sprite = figure.inventoryIcon;
                slotIcons[i].enabled = true;
                return true;
            }
        }
        return false;
    }

    public void SwitchItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length) return;
        // if (slots[slotIndex] == null) return;
        
        activeSlot = slotIndex;
        if (slots[slotIndex] != null)
        {
            playerInteraction.SetHeldFigure(slots[slotIndex]);
        }
        else
        {
            playerInteraction.SetHeldFigure(null);
        }
    }

    public void RemoveItem(int slotIndex)
    {
        if (slots[slotIndex] == null) return;
        
        slots[slotIndex] = null;
        slotIcons[slotIndex].sprite = null;
        slotIcons[slotIndex].enabled = false;
    }
}
