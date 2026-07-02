using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    // Simpan key yang lagi dipegang
    public string heldKey = "";

    public void PickUpKey(string keyID)
    {
        heldKey = keyID;
        Debug.Log("Picked up key: " + keyID);
    }

    public bool HasKey(string keyID)
    {
        return heldKey == keyID;
    }

    public void RemoveKey()
    {
        heldKey = "";
    }
}