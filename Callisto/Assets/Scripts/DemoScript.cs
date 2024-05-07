using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public Item[] itemsToPickup;

    public Item item;

    private bool TryCraftingRecipe(Recipe recipe)
    {
        string details;
        bool craft =
            inventoryManager.CheckRecipeIngredients(recipe, out details);
        if (craft)
        {
            Debug.Log("Mozna stworzyc kilofa");
            return craft;
        }
        else
        {
            Debug.Log("Nie mozna stworzyc kilofa");
            Debug.Log (details);
            return craft;
        }
    }

    public void PickupItem(Item item)
    {
        if (item.itemID >= 0 && item.itemID < itemsToPickup.Length)
        {
            if (!inventoryManager.IsInventoryFull())
            {
                if (inventoryManager.AddItem(itemsToPickup[item.itemID]))
                {
                    Debug.Log($"{item.itemName} zostal dodany");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log($"{item.itemName} nie zostal dodany");
                }
            }
            else
            {
                Debug.Log("Ekwipunek jest pelny");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupItem (item);
        }
    }
}
