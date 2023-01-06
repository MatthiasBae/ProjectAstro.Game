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
    
    private void Awake() {
        this.RegisterEvents();
        this.UpdateUI();
    }

    private void RegisterEvents() {
        this.Inventory.ItemAdded += (inventorySlot) => {
            this.GridUI.AddItem(inventorySlot.InventoryItem, inventorySlot.Position);
        };
        this.Inventory.ItemRemoved += (inventoryItem) => {
            //@TODO: Gewicht updaten
            this.GridUI.RemoveItem(inventoryItem);
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
        this.GridUI.AddItems(this.Inventory);

        var height = this.Inventory.Config.Size.y * 40 + 120;
        this.UpdateSize(new Vector2(400, height));
    }

    private void UpdateSize(Vector2 size) {
        this.RectTransform.sizeDelta = size;
        
    }
}
