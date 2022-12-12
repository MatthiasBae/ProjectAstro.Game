using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    [SerializeField]
    private RectTransform RectTransform;

    [SerializeField]
    private RectTransform ItemCountRectTransform;

    [SerializeField]
    private RectTransform ItemIconRectTransform;
    
    public InventoryItem InventoryItem;
    
    [SerializeField]
    private TMP_Text ItemCountText;

    [SerializeField]
    private Image ItemImage;

    public ItemUI(InventoryItem inventoryItem) {
        this.InventoryItem = inventoryItem;
    }

    public void Initialize(GridLayoutGroup slotGrid) {
        this.UpdateSize(slotGrid);
        this.UpdateItemCount(this.InventoryItem.Quantity);
    }

    public void UpdateSize(GridLayoutGroup slotGrid) {
        if(this.InventoryItem == null) {
            return;
        }

        var itemSlots = new Vector2(this.InventoryItem.Item.Config.Inventory.SlotsX, this.InventoryItem.Item.Config.Inventory.SlotsY);
        var cellSize = slotGrid.cellSize;
        var size = new Vector2(itemSlots.x * cellSize.x, itemSlots.y * cellSize.y);
        
        this.RectTransform.sizeDelta = size;
        this.ItemCountRectTransform.sizeDelta = size;
        this.ItemIconRectTransform.sizeDelta = size;
    }

    public void UpdateItemCount(int count) {
        this.ItemCountText.text = count.ToString("00");
    }

}
