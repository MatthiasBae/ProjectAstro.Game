using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    [SerializeField] 
    private RectTransform Rect;
    [SerializeField]
    private ItemContainerUIBase GridUI;
    
    public ItemContainerBase Inventory;
    
    [SerializeField]
    private Vector2 GridSlotSize;
    [SerializeField]
    private GameObject InventoryItemPrefab;
    [SerializeField] 
    private GameObject ItemContainer;

    public event Action ItemPlaced;
    public event Action ItemRemoved;
    
    public bool Visible{
        get => this.gameObject.activeSelf;
        set => this.gameObject.SetActive(value);
    }
    
    private void RegisterEvents(){
        this.GridUI.ItemDropped += this.TryAddItem; 
        this.GridUI.ItemSelected += this.TrySelectItem;
        
        this.Inventory.ItemAdded += this.PlaceItem;
        this.Inventory.ItemRemoved += this.RemoveItem;
    }
    
    private void UnregisterEvents(){
        this.GridUI.ItemDropped -= this.TryAddItem; 
        this.GridUI.ItemSelected -= this.TrySelectItem;
        
        this.Inventory.ItemAdded -= this.PlaceItem;
        this.Inventory.ItemRemoved -= this.RemoveItem;
    }
    
    public void InitializeInventory(ItemContainerBase inventory){
        this.Inventory = inventory;
        this.GridUI.CreateSlots(this.Inventory.Config.Size);
        this.SetSize(this.GridSlotSize,this.Inventory.Config.Size);
        this.RegisterEvents();
        this.PlaceItems();
        this.Visible = true;
    }
    
    public void PlaceItem(Item item, Vector2 position){
        var inventoryItemUI = (InventoryItemUI)null;
        if (InventoryUIController.Instance.HasItemSelected){
            if (InventoryUIController.Instance.SelectedInventoryItemUI.Item == item){
                inventoryItemUI = InventoryUIController.Instance.SelectedInventoryItemUI;
            }
            else{
                inventoryItemUI = InventoryItemUI.Create(this.InventoryItemPrefab, item, this.GridSlotSize, this.ItemContainer);
            }
        }
        else{
            inventoryItemUI = InventoryItemUI.Create(this.InventoryItemPrefab, item, this.GridSlotSize, this.ItemContainer);
        }

        this.GridUI.PlaceItem(inventoryItemUI, position);
        this.ItemPlaced?.Invoke();
    }

    public void RemoveItem(Item item, Vector2 slotPosition){
        var inventoryItemUI = this.GridUI.GetItem(slotPosition);
        if (inventoryItemUI is null){
            return;
        }
        if (InventoryUIController.Instance.SelectedInventoryItemUI == inventoryItemUI){
            this.GridUI.RemoveItem(inventoryItemUI);
        }
        else{
            this.GridUI.DestroyItem(inventoryItemUI);
        }
        this.ItemRemoved?.Invoke();
    }
    
    public void TryAddItem(InventoryItemUI inventoryItemUI, Vector2 slotPosition){
        var item = inventoryItemUI.Item;
        var success = this.Inventory.TryAddItemAt(item, slotPosition);
        
    }
    public void TrySelectItem(InventoryItemUI inventoryItemUI){
        if(inventoryItemUI is null){
            this.ResetItems();
            this.UnregisterEvents();
            return;
        }
        
        var item = inventoryItemUI.Item;
        var selected = InventoryUIController.Instance.TrySelectItem(inventoryItemUI);
        if (!selected){
            return;
        }
        
        this.Inventory.TryRemoveItem(item);
    }
    
    public void SetInventory(ItemContainerBase inventory){
        this.ResetUIItems();
        if (this.Inventory is not null){
            this.ResetItems();
            this.UnregisterEvents();
        }

        if (inventory is null){
            this.Visible = false;
            return;
        }
        this.InitializeInventory(inventory);
    }

    private void ResetItems(){
        this.GridUI.Reset();
    }
    
    private void ResetUIItems(){
        this.GridUI.ResetUI();
    }
    
    private void PlaceItems(){
        foreach(var item in this.Inventory.Items){
            this.PlaceItem(item.Key, item.Value);
        }
    }
    
    public void SetSize(Vector2 gridSlotSize, Vector2 slotCount) {
        var size = new Vector2{
            x = slotCount.x * gridSlotSize.x,
            y = slotCount.y * gridSlotSize.y
        };
        this.Rect.sizeDelta = size;
    }
}
