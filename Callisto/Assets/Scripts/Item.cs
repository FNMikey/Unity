using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int maxStack = 5;
    public string iconPath;

    void Start()
    {
       
        if (!string.IsNullOrEmpty(iconPath))
        {
            Sprite icon = Resources.Load<Sprite>(iconPath);
            if (icon != null)
            {
         
                GetComponent<SpriteRenderer>().sprite = icon;
            }
            else
            {
                Debug.LogWarning($"Icon not found at path: {iconPath}");
            }
        }
    }

    void Update()
    {
        
    }
}
