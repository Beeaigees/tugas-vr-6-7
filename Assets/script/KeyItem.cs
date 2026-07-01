using TMPro;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [Header("Key Info")]
    public string keyID = "exit_key";
    public string keyName = "Exit Key";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statusText;

    public void PickUp()
    {
        // Cari KeyHolder
        KeyHolder holder = FindObjectOfType<KeyHolder>();
        if (holder != null)
            holder.PickUpKey(keyID);

        // Cari PlayerInventory — masukin ke slot
        PlayerInventory inv = FindObjectOfType<PlayerInventory>();
        if (inv != null)
        {
            bool added = inv.AddItem(keyID);
            if (!added)
            {
                if (statusText != null)
                    statusText.text = "⚠ Inventory Full!";
                return; // gak jadi diambil kalau penuh
            }
        }

        if (statusText != null)
            statusText.text = "✓ Picked up: " + keyName;

        // Sembunyiin kunci
        gameObject.SetActive(false);
        Debug.Log("Key picked up: " + keyID);
    }
}