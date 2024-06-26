using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public string itemName;

    public int itemID;

    public ItemType type;

    public ActionType actionType;

    public Vector2Int range = new Vector2Int(5, 4);

    public bool stackable = true;

    public Sprite image;
}

public enum ItemType
{
    BuildingBlock,
    Tool
}

public enum ActionType
{
    Dig,
    Build,
    Cut
}
