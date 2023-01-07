using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryDragDropUIController : MonoBehaviour {
    
    public InventoryUI SelectedItemUIInventory;
    public InventoryItemUI SelectedUIItem;
    public InventoryGridUI SelectedUIGrid;
    public GameObject DraggedItemContainer;

    public event Action<InventoryItemUI> ItemSelected;
    public event Action<InventoryGridUI> GridSelected;

    private void Update() {
        if(this.SelectedUIItem == null) {
            return;
        }


        this.SelectedUIItem.SetPosition(Input.mousePosition);
    }

    public void SelectItem(InventoryItemUI item) {
        item.gameObject.transform.SetParent(this.DraggedItemContainer.transform);
        item.EnableRaycastTarget(false);
        this.SelectedUIItem = item;
        this.SelectedItemUIInventory = item.InventoryUI;
        
        this.ItemSelected?.Invoke(item);
    }
    public void UnselectItem(InventoryItemUI item) {
        if(this.SelectedUIItem != item) {
            //@TODO: Hier dann Anfang der Prüfung , ob ein Item Stackable ist
            return;
        }
        
        this.SelectedUIItem = null;
    }

    public void SelectGrid(InventoryGridUI grid) {

        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(grid.SlotContainerRectTransform, Input.mousePosition, null, out localPosition);

        localPosition.y = -localPosition.y;
        localPosition += grid.SlotContainerRectTransform.sizeDelta / 2;
        
        var clickedSlotPosition = grid.LocalPositionToSlotPosition(localPosition);
        grid.OnItemDropped(this.SelectedUIItem, clickedSlotPosition);
        this.SelectedUIGrid = grid;

        this.GridSelected?.Invoke(grid);
    }
    
}
