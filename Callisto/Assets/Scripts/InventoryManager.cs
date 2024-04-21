using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;

    public InventorySlot[] inventorySlots;

    public GameObject inventoryItemPrefab;

    public bool RemoveItem(string itemName, int quantity)
    {
        int remainingQuantity = quantity;

        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot =
                slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.name == itemName)
            {
                if (itemInSlot.count >= remainingQuantity)
                {
                    itemInSlot.count -= remainingQuantity;
                    if (itemInSlot.count == 0)
                    {
                        Destroy(itemInSlot.gameObject); // Destroy the item if its count drops to zero
                    }
                    itemInSlot.RefreshCount();
                    Debug.Log($"Usunieto {quantity} o nazwie '{itemName}'. W miejscu: {itemInSlot.count}");
                    return true;
                }
                else
                {
                    remainingQuantity -= itemInSlot.count;
                    Destroy(itemInSlot.gameObject); // Destroy the item as it's now depleted
                }
            }
        }

        Debug.Log($"Nie usunieto pemnej ilosci '{itemName}'. Potrzeba jest: {quantity}, Usunieto: {quantity - remainingQuantity}");
        return false;
    }

    public bool IsInventoryFull()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot =
                slot.GetComponentInChildren<InventoryItem>();
            if (
                itemInSlot == null ||
                itemInSlot.count < maxStackedItems && itemInSlot.item.stackable
            )
            {
                return false;
            }
        }
        return true;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot =
                slot.GetComponentInChildren<InventoryItem>();
            if (
                itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable
            )
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot =
                slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem (item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem (item);
    }

    public bool
    CheckRecipeIngredients(Recipe recipe, out string inventoryContents)
    {
        Dictionary<string, int> inventoryCounts = new Dictionary<string, int>();
        inventoryContents = "";
        bool canCraft = true;
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
            InventoryItem itemInSlot =
                slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null)
            {
                if (inventoryCounts.ContainsKey(itemInSlot.item.itemName))
                {
                    inventoryCounts[itemInSlot.item.itemName] +=
                        itemInSlot.count;
                }

                if (itemInSlot.item.stackable)
                {
                    inventoryContents +=
                        $"Slot {i}: {itemInSlot.item.itemName}, Ilość: {itemInSlot.count}\n";
                }
                else
                {
                    inventoryContents +=
                        $"Slot {i}: {itemInSlot.item.itemName}\n";
                }
            }
            else
            {
                inventoryContents += $"Slot {i}: Pusty\n";
            }
        }

        // Add summary information about required items from the recipe and check if they can be crafted
        inventoryContents +="\nPodsumowanie ilości wymaganych przedmiotów z przepisu:\n";
        foreach (Ingredient ingredient in recipe.ingredients)
        {
            int countNeeded = ingredient.quantity;
            int countAvailable = inventoryCounts[ingredient.name];
            inventoryContents +=
                $"{ingredient.name}: Potrzeba {countNeeded}, Dostępne {countAvailable}\n";
            if (countAvailable < countNeeded)
            {
                canCraft = false;
            }
        }

        return canCraft;
    }

    public bool TryCraftItem(Recipe recipe)
    {
        string inventoryContents;
        if (CheckRecipeIngredients(recipe, out inventoryContents))
        {
            Debug.Log("Item jest tworzony, sa skladniki");
            foreach (Ingredient ingredient in recipe.ingredients)
            {
                RemoveItem(ingredient.name, ingredient.quantity);
            }
            Debug.Log("Przedmiot zostal stworzony aby item dzialal");
            return true;
        }
        else
        {
            Debug.Log("Nie mozna stworzyc przedmiotu zamalo skladnikow");
            Debug.Log (inventoryContents);
            return false;
        }
    }
}
