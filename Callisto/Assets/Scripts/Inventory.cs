using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item item;
        public int quantity;
        public Image itemIcon;
        public Text quantityText;
    }

    public InventorySlot[] slots = new InventorySlot[4];

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Miecz") || other.gameObject.CompareTag("Siekiera") ||other.gameObject.CompareTag("Kamien") || other.gameObject.CompareTag("Patyk") )
        {
            Item item = other.gameObject.GetComponent<Item>();
            if (item != null) 
            {
                Debug.Log("Item tag found and component exists");
                AddItem(item);
                Destroy(other.gameObject); 
            }
            else
            {
                Debug.Log("Component missing on tagged object.");
            }
        }
        else
        {
            Debug.Log("Item tag not found or component missing");
        }
    }

    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd == null) 
        {
            Debug.LogWarning("Attempted to add a null item.");
            return;
        }

        foreach (var slot in slots)
        {
            if (slot.item != null && slot.item.itemName == itemToAdd.itemName && slot.quantity < slot.item.maxStack)
            {
                
                slot.quantity++;
                UpdateSlotUI(slot);
                Debug.Log($"Added more {itemToAdd.itemName} to the inventory. Slot now has {slot.quantity} items.");
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.item == null)
            {

                slot.item = itemToAdd;
                slot.quantity = 1;
                UpdateSlotUIWithSprite(slot, itemToAdd);
                Debug.Log($"Added {itemToAdd.itemName} to the inventory.");
                return;
            }
        }

        Debug.Log("Inventory is full. Could not pick up item.");
    }

    private void UpdateSlotUI(InventorySlot slot)
    {
        if (slot.quantityText != null)
        {
            slot.quantityText.text = slot.quantity.ToString();
        }
        else
        {
            Debug.LogWarning("Quantity text is not set for a slot.");
        }
    }

    private void UpdateSlotUIWithSprite(InventorySlot slot, Item itemToAdd)
    {
  
        UpdateSlotUI(slot); 
        if (slot.itemIcon != null)
        {
            SpriteRenderer renderer = itemToAdd.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                slot.itemIcon.sprite = renderer.sprite;
                slot.itemIcon.enabled = true; 
            }
            else
            {
                slot.itemIcon.enabled = false;
                Debug.LogWarning("SpriteRenderer is missing on itemToAdd, item icon disabled.");
            }
        }
        else
        {
            Debug.LogWarning("Item icon is not set for a slot.");
        }
    }
}
