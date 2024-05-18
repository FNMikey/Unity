using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject uiPanel; 
        GameObject[] nearbyChests;
        public InventorySlot[] slots;

// Wywołanie metody

void Start() {
    if (uiPanel != null) {
        uiPanel.SetActive(false);
    }
}

void Update() {
    if (Input.GetKeyDown(KeyCode.N)) {
        ToggleUIPanel();
    }
     if (Input.GetKeyDown(KeyCode.E)) {  // Assuming 'E' is the interact key
        CheckNearbyChestContents();
    }
}

void ToggleUIPanel() {
    if (uiPanel != null) {
        uiPanel.SetActive(!uiPanel.activeSelf);
    }
}

public GameObject player; 
    public float proximityDistance = 5f; 
 public bool CheckChestProximity(out GameObject[] chests)
{
    List<GameObject> foundChests = new List<GameObject>();
    GameObject[] allChests = GameObject.FindGameObjectsWithTag("Chest");

    foreach (GameObject chest in allChests)
    {
        if (Vector3.Distance(player.transform.position, chest.transform.position) <= proximityDistance)
        {
            foundChests.Add(chest);
        }
    }

    chests = foundChests.ToArray();
    return chests.Length > 0;
}

public void CheckNearbyChestContents()
{
    GameObject[] nearbyChests;
    if (CheckChestProximity(out nearbyChests))
    {
        foreach (GameObject chest in nearbyChests)
        {
            InventoryManager chestInventoryManager = chest.GetComponent<InventoryManager>();
            if (chestInventoryManager != null)
            {
                string inventoryContents;
                if (CheckChestInventory(chestInventoryManager.inventorySlots, out inventoryContents))
                {
                    Debug.Log("Contents of the chest: " + inventoryContents);
                }
                else
                {
                    Debug.Log("Chest is empty or contents are not usable.");
                }
            }
            else
            {
                Debug.Log("No InventoryManager found on the chest. Checking GameObject: " + chest.name);
            }
        }
    }
    else
    {
        Debug.Log("No chests are within proximity.");
    }
}


public bool CheckChestInventory(InventorySlot[] slots, out string inventoryContents)
{
    Dictionary<string, int> inventoryCounts = new Dictionary<string, int>();
    inventoryContents = "";

    foreach (InventorySlot slot in slots)
    {
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            inventoryContents += $"Slot {slot.name}: {itemInSlot.item.itemName}, Quantity: {itemInSlot.count}\n";
            if (inventoryCounts.ContainsKey(itemInSlot.item.itemName))
            {
                inventoryCounts[itemInSlot.item.itemName] += itemInSlot.count;
            }
            else
            {
                inventoryCounts[itemInSlot.item.itemName] = itemInSlot.count;
            }
        }
        else
        {
            inventoryContents += $"Slot {slot.name}: Empty\n";
        }
    }
    return inventoryCounts.Count > 0; // Returns true if there's at least one item
}

}
