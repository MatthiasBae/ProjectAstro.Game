using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryUIController {

    public List<GameObject> InventoryUIList;

    public void ExchangeInventory(string actorModuleName, Inventory inventory) {
        foreach(var inventoryUIGameObject in this.InventoryUIList) {
            if(inventoryUIGameObject.name.Contains(actorModuleName)) {
                var inventoryUI = inventoryUIGameObject.GetComponent<InventoryUI>();
                inventoryUI.Initialize(inventory.Config);
            }
        }
    }
    
}
