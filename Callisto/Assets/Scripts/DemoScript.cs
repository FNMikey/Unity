using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public Item item;

    public void PickupItem(Item item)
    {
        if (item.itemID >= 0 && item.itemID < itemsToPickup.Length)
        {
            if (inventoryManager.AddItem(itemsToPickup[item.itemID]))
            {
                Debug.Log("ITEM ADDED");
            }
            else
            {
                Debug.Log("ITEM NOT ADDED");
                Debug.Log("ITEM NOT ADDEasdasD");
                Debug.Log(inventoryManager.PrintInventoryContents());
                Recipe pickaxeRecipe = new Recipe();
                pickaxeRecipe.ingredients = new List<Ingredient> {
        new Ingredient { name="Stick", quantity = 2 },
        new Ingredient { name="Rock", quantity = 3 }
    };
                string result = inventoryManager.CheckRecipeIngredients(pickaxeRecipe);
                Debug.Log($"Zostanie stworzony kilof {result}");
                Debug.Log(result);
                Debug.Log(inventoryManager.RemoveItem("Rock", 3));
                Debug.Log(inventoryManager.RemoveItem("Stick", 2));
 

            }
        }
        else
        {
            Debug.Log("Invalid itemID: " + item.itemID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PickupItem(item);
            gameObject.SetActive(false);
        }
    }
}
