using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsUI : MonoBehaviour {
    public RectTransform RectTransform;
    public Dictionary<ItemUI, List<Vector2>> Items;
    public GameObject ItemUIPrefab;

    
    public void CreateUIItems(Inventory inventory) {
        this.Items = new Dictionary<ItemUI, List<Vector2>>();
        
        
    }

    public void AddUIItem(InventoryItem inventoryItem, List<Vector2> coordinateList) {
        
    }
}
