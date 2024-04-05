using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    // Start is called before the first frame update

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }
    private void OnEnable()
    {
        inventory.InitialCheckoutText();
        inventory.UpdateCheckoutInfo();
    }
}
