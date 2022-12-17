using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    [SerializeField]
    private Inventory Inventory;

    [SerializeField]
    private InventoryGridUI GridUI;

    [SerializeField]
    private InventoryHeaderUI HeaderUI;

    [SerializeField]
    private RectTransform RectTransform;
    
    private void Start() {
        this.UpdateUI();
    }

    public void ExchangeInventory(ref Inventory inventory) {
        var currentInventory = this.Inventory;
        
        

        this.Inventory = inventory;
        this.UpdateUI();
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
        this.GridUI.AddItems(this.Inventory);

    }

    public void UpdateGridSlotsUI() {
        this.GridUI.ClearSlots();
        this.GridUI.InitializeSlots(this.Inventory);
    }

    public void UpdateGridItemsUI() {
        foreach(var item in this.Inventory.ItemPositions) {
            var coordinates = item.Value;
            var inventoryItem = item.Key;
            
            this.GridUI.AddItem(inventoryItem, coordinates);
        }
    }
}
