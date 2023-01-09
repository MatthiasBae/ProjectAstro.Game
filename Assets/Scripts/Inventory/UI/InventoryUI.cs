using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour {
    
    [SerializeField]
    private InventoryDragDropUIController DragDropController;

    public Inventory Inventory;

    //public InventoryGridUI GridUI;
    //public InventoryHeaderUI HeaderUI;

    public InventoryGridUI Grid;
    public InventoryHeaderUI Header;
    [SerializeField]
    private RectTransform RectTransform;
    
    private void Awake() {
        this.RegisterEvents();
        //this.UpdateUI();
        this.Render();
    }

    //@TODO: Eventuell noch ein UnregisterEvents machen wenn das Inventar ausgetauscht wird?
    private void RegisterEvents() {


        //this.Inventory.ItemAdded += (inventorySlot) => {
        //    var inventoryItemAdded = inventorySlot.InventoryItem;
        //    var inventoryUIItemSelected = this.DragDropController.SelectedUIItem;
        //    var inventoryItemSelected = inventoryUIItemSelected?.InventoryItem;
        //    var position = inventorySlot.Position;

        //    if(inventoryUIItemSelected != null && inventoryItemSelected == inventoryItemAdded) {
        //        this.GridUI.AddItem(inventoryUIItemSelected, position);
        //        return;
        //    }
        //    this.GridUI.AddItem(inventoryItemAdded, position, this.DragDropController, this);
        //};
        
        this.Inventory.ItemAddedTry += (inventoryItem, slotCoordinates, success) => {
            if(!success) {
                return;
            }
            this.AddItemUI(inventoryItem, slotCoordinates);
        };

        this.Inventory.ItemRemoved += (inventoryItem) => {
            this.RemoveItemUI(inventoryItem);
        };

        this.Grid.ItemDropped += (inventoryItemUI, slotCoordinates) => {
            var inventoryItem = inventoryItemUI.InventoryItem;

            var added = this.Inventory.TryAddItemAt(inventoryItem, slotCoordinates);
            if(added) {
                Debug.Log($"InventoryItem in Inventar {this.Inventory.Config.Name} nicht hinzugefügt");

                this.DragDropController.SelectGrid(this.Grid);
            }
        };

        this.Grid.ItemSelected += (inventoryItemUI) => {
            var inventoryItem = inventoryItemUI.InventoryItem;
            Debug.Log($"InventoryItem aus Inventar {this.Inventory.Config.Name} ausgewählt");
            this.Inventory.RemoveItem(inventoryItem);
        };
    }

    public void Render() {
        var weight = this.Inventory.Weight;
        var maxWeight = this.Inventory.Config.MaxWeight;
        var name = this.Inventory.Config.Name;
        var size = this.Inventory.Config.Size;

        this.Header.Render(name, weight, maxWeight);
        this.Grid.Render(size);

        var height = (this.Inventory.Config.Size.y + 1) * 40;
        this.UpdateSize(new Vector2(400, height));
    }

    private void AddItemUI(InventoryItem inventoryItem, Vector2 slotCoordinates) {
        var selectedUIInventoryItem = this.DragDropController?.SelectedUIItem;

        if(selectedUIInventoryItem == null) {
            this.Grid.AddItem(inventoryItem, slotCoordinates, this.DragDropController, this);
            return;
        }

        var selectedInventoryItem = selectedUIInventoryItem.InventoryItem;
        if(inventoryItem != selectedInventoryItem) {
            this.Grid.AddItem(inventoryItem, slotCoordinates, this.DragDropController, this);
            return;
            
        }

        if(inventoryItem == selectedInventoryItem) {
            this.Grid.AddItem(selectedUIInventoryItem, slotCoordinates, this);
            return;
        }

    }

    public void RemoveItemUI(InventoryItem inventoryItem) {
        var selectedUIInventoryItem = this.DragDropController?.SelectedUIItem;

        if(selectedUIInventoryItem == null) {
            this.Grid.RemoveItem(inventoryItem, true);
            return;
        }

        var selectedInventoryItem = selectedUIInventoryItem.InventoryItem;
        if(inventoryItem != selectedInventoryItem) {
            this.Grid.RemoveItem(inventoryItem, true);
            return;

        }

        if(inventoryItem == selectedInventoryItem) {
            this.Grid.RemoveItem(inventoryItem, false);
            return;
        }
    }

    private void UpdateSize(Vector2 size) {
        this.RectTransform.sizeDelta = size;
        
    }
}
