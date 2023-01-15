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


    private void Update() {
        if(this.SelectedUIItem == null) {
            return;
        }


        this.SelectedUIItem.SetPosition(Input.mousePosition);
    }

    //@TODO: Auch noch refactorieren
    public void SelectItem(InventoryItemUI item) {
        item.gameObject.transform.SetParent(this.DraggedItemContainer.transform, false);
        item.EnableRaycastTarget(false);
        this.SelectedUIItem = item;
        this.SelectedItemUIInventory = item.InventoryUI;
        
        this.SelectedItemUIInventory.Grid.OnItemSelected(item);
    }
    public void UnselectItem(InventoryItemUI item) {
        if(this.SelectedUIItem != item) {
            //@TODO: Hier dann Anfang der Prüfung , ob ein Item Stackable ist
            return;
        }
        
        this.SelectedUIItem.gameObject.transform.SetParent(this.SelectedUIGrid.ItemContainerRectTransform.transform);
        
        this.SelectedUIItem.EnableRaycastTarget(true);

        this.SelectedUIItem.InventoryUI = item.InventoryUI;
        this.SelectedUIItem = null;
        this.SelectedItemUIInventory = null;
    }

    public void SelectGrid(InventoryGridUI grid) {
        
        this.SelectedUIGrid = grid;
        this.UnselectItem(this.SelectedUIItem);
    }
}
