using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
    public Item Item;
    public int Quantity;
    public float Weight => this.Item.Config.Weight * this.Quantity;
    public static InventoryItem Create(Item item, int quantity) {
        return new InventoryItem() {
            Item = item,
            Quantity = quantity
        };
    }
}
