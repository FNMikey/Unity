using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public void CraftPick()
    {
        Recipe pickaxeRecipe = new Recipe();
        pickaxeRecipe.ingredients =
            new List<Ingredient> {
                new Ingredient { name = "Stick", quantity = 2 },
                new Ingredient { name = "Rock", quantity = 3 }
            };
        bool success = inventoryManager.TryCraftItem(pickaxeRecipe);
        if (success)
        {
            Debug.Log("Kilof zostal stworzony");
        }
        else
        {
            Debug.Log("Kilof nie zostal stworzony");
        }
    }
}
