using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
    public Item Item;
    public int Quantity;

    public static InventoryItem Create(Item item, int quantity) {
        return new InventoryItem() {
            Item = item,
            Quantity = quantity
        };
    }

    public bool TryAddQuantity(int quantity) {
        var maxStackSize = this.Item.Config.Inventory.MaxStackSize;
        var newQuantity = this.Quantity + quantity;

        if(newQuantity > maxStackSize || newQuantity < 0) {
            return false;
        }
        
        this.Quantity = newQuantity;
        return true;
    }
}
