using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour {
    
    [SerializeField]
    private InventoryDragDropUIController DragDropController;

    public Inventory Inventory;

    public InventoryGridUI GridUI;
    public InventoryHeaderUI HeaderUI;

    [SerializeField]
    private RectTransform RectTransform;
    
    private void Awake() {
        this.RegisterEvents();
        this.UpdateUI();
    }

    private void RegisterEvents() {
        this.Inventory.ItemAdded += (inventorySlot) => {
            this.GridUI.AddItem(inventorySlot.InventoryItem, inventorySlot.Position, this.DragDropController, this);
        };
        this.Inventory.ItemRemoved += (inventoryItem) => {
            //@TODO: Gewicht updaten
            this.GridUI.RemoveItem(inventoryItem);
        };

        //@TODO: Event hier registrieren welches bei Click auf 
        this.GridUI.ItemDropped += (info) => {
            var position = info.Value;
            var inventoryItemUI = info.Key;

            
            var addedSuccessfully = this.Inventory.TryAddItemAt(inventoryItemUI.InventoryItem.Item, inventoryItemUI.InventoryItem.Quantity, position);
            if(!addedSuccessfully) {
                return;
            }
            
            this.DragDropController.SelectedItemUIInventory.Inventory.RemoveItem(inventoryItemUI.InventoryItem.Item);
        };

        this.DragDropController.ItemSelected += (itemUI) => {
            
        };
    }
    

    public void UpdateUI() {
        if(this.Inventory.Config == null || this.Inventory == null) {
            Debug.LogError("Inventory or InventoryConfig is null");
            return;
        }
        
        this.HeaderUI.SetName(this.Inventory.Config.Name);
        this.HeaderUI.SetWeight(this.Inventory.Weight, this.Inventory.Config.MaxWeight);
        
        this.GridUI.ClearSlots();
        this.GridUI.InitializeSlots(this.Inventory);

        this.GridUI.ClearItems();
        this.GridUI.AddItems(this, this.DragDropController);

        var height = this.Inventory.Config.Size.y * 40 + 120;
        this.UpdateSize(new Vector2(400, height));
    }

    private void UpdateSize(Vector2 size) {
        this.RectTransform.sizeDelta = size;
        
    }
}
