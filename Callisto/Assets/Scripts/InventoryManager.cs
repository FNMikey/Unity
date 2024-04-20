using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    
    public bool AddItem(Item item)
{
    for (int i = 0; i < inventorySlots.Length; i++)
    {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable)
        {
            itemInSlot.count++;
            itemInSlot.RefreshCount();
            return true;
        }
    }
    for (int i = 0; i < inventorySlots.Length; i++)
    {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot == null)
        {
            SpawnNewItem(item, slot);
            return true;
        }
    }
    return false;
}

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public string PrintInventoryContents()
{
    string inventoryContents = "";
    for (int i = 0; i < inventorySlots.Length; i++)
    {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            if (itemInSlot.item.stackable)
            {
                
                inventoryContents += $"Slot {i}: {itemInSlot.item.name}, Ilość: {itemInSlot.count}\n";
            }
            else
            {
            
                inventoryContents += $"Slot {i}: {itemInSlot.item.name}\n";
            }
        }
        else
        {
            inventoryContents += $"Slot {i}: Pusty\n";
        }
    }
    return inventoryContents;
}


public string CheckRecipeIngredients(Recipe recipe)
{
    Dictionary<string, int> inventoryCounts = new Dictionary<string, int>();
    string inventoryContents = "";

    foreach (Ingredient ingredient in recipe.ingredients)
    {
        if (!inventoryCounts.ContainsKey(ingredient.name))
        {
            inventoryCounts[ingredient.name] = 0;
        }
    }

    for (int i = 0; i < inventorySlots.Length; i++)
    {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {

            if (inventoryCounts.ContainsKey(itemInSlot.item.itemName)) 
            {
                inventoryCounts[itemInSlot.item.itemName] += itemInSlot.count;
            }

            if (itemInSlot.item.stackable)
            {
                inventoryContents += $"Slot {i}: {itemInSlot.item.itemName}, Ilość: {itemInSlot.count}\n";
            }
            else
            {
                inventoryContents += $"Slot {i}: {itemInSlot.item.itemName}\n";
            }
        }
        else
        {
            inventoryContents += $"Slot {i}: Pusty\n";
        }
    }

    inventoryContents += "\nPodsumowanie ilości wymaganych przedmiotów z przepisu:\n";
    foreach (Ingredient ingredient in recipe.ingredients)
    {
        int countNeeded = ingredient.quantity;
        int countAvailable = inventoryCounts[ingredient.name];
        inventoryContents += $"{ingredient.name}: Potrzeba {countNeeded}, Dostępne {countAvailable}\n";
    }

    return inventoryContents;
}


public bool RemoveItem(string itemName, int quantity)
{
    for (int i = 0; i < inventorySlots.Length; i++)
    {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null && itemInSlot.item.name == itemName)
        {
            if (itemInSlot.item.stackable)
            {

                itemInSlot.count -= quantity;
                itemInSlot.RefreshCount();

    
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                    Debug.Log($"Usunięto '{itemName}' z ekwipunku, brak pozostałych '{itemName}'.");
                } else {
                    Debug.Log($"Zaktualizowano ilość '{itemName}' o {quantity} mniej, aktualna ilość: {itemInSlot.count}.");
                }
                return true; 
            }
            else if (quantity == 1) 
            {
                Destroy(itemInSlot.gameObject);
                Debug.Log($"Usunięto '{itemName}' z ekwipunku.");
                return true;
            }
        }
    }
    Debug.Log($"Nie usunięto '{itemName}' z ekwipunku, brak wystarczającej ilości.");
    return false; 
}


}


