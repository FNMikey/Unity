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
            Debug.Log("ITEM ADDED");
        }
        else
        {
            Debug.Log("ITEM NOT ADDED");
        }
    }
}
