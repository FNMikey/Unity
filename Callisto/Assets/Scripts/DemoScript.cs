using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        if (inventoryManager.AddItem(itemsToPickup[id]))
        {
            // AddItem returned true, so an item was successfully added
            Debug.Log("ITEM ADDED");
        }
        else
        {
            // AddItem returned false, so the item was not added
            Debug.Log("ITEM NOT ADDED");
        }
    }
}
