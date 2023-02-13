using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Item {
    public ItemConfig Config;
    public int Amount;
    
    public ItemContainerBase Inventory;
    
    public static Item Create(ItemConfig itemConfig, int amount = 1) {
        var item = new Item {
            Config = itemConfig,
            Amount = amount
        };
        item.InitializeInventory();
        return item;
    }
    
    public void InitializeInventory(){
        if(this.Config.InventoryConfig is null) {
            return;
        }
        this.Inventory = new Inventory(this.Config.InventoryConfig);
    }
}
