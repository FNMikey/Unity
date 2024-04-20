using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu( menuName = "Scriptable object/Recipes")]

public class Recipe : ScriptableObject {
    public List<Ingredient> ingredients;
    public Item result;
}

[System.Serializable]
public class Ingredient {
    public string name;
    public int quantity;
}