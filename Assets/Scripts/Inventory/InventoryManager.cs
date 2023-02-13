using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class InventoryManager {
    public Dictionary<string, ItemContainerBase> Inventories;
    
    public InventoryManager(){
        this.Inventories = new Dictionary<string, ItemContainerBase>();
    }
    
    public void AddInventory(string actorPartName, ItemContainerBase inventory){
        var existingInventory = this.GetInventory(actorPartName);
        if (existingInventory != null){
            return;
        }
        
        this.Inventories.Add(actorPartName, inventory);
    }
    
    public ItemContainerBase GetInventory(string actorPartName){
        return this.Inventories.ContainsKey(actorPartName) 
            ? this.Inventories[actorPartName] 
            : null;
    }
}
