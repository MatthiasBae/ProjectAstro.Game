using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public InventoryInfoUI InventoryInfoUI;
    public InventorySlotsUI InventorySlotsUI;
    public InventoryItemsUI InventoryItemsUI;
    public void Initialize(InventoryConfig inventoryConfig) {
        this.CreateUI(inventoryConfig);
        this.SetNameText(inventoryConfig.Name);
        this.SetWeightText(inventoryConfig.MaxWeight, 0);
    }

    private void CreateUI(InventoryConfig inventoryConfig) {
        this.ResetUI();
        this.CreateSlots(inventoryConfig);
        
    }
    public void ResetUI() {
        this.InventoryInfoUI.ResetInfo();
        this.InventorySlotsUI.ResetSlots();
    }

    public void SwitchInventory(Inventory inventory) {
        this.CreateUI(inventory.Config);
        this.SetNameText(inventory.Config.Name);
        this.SetWeightText(inventory.Config.MaxWeight, 0);
    }
    
    private void CreateSlots(InventoryConfig inventoryConfig) {
        this.InventorySlotsUI.CreateSlots(inventoryConfig);
    }
    private void CreateItems(Inventory inventory) {
        //this.InventoryItemsUI.CreateItems(inventory);
    }
    public void SetNameText(string name) {
        this.InventoryInfoUI.SetNameText(name);
    }
    public void SetWeightText(float maxWeight, float actualWeight) {
        this.InventoryInfoUI.SetWeightText(maxWeight, actualWeight);
    }
}
