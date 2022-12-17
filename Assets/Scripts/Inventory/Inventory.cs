using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {
    public InventoryConfig Config;
    public Dictionary<Vector2, InventoryItem> ItemSlots;
    public Dictionary<InventoryItem, List<Vector2>> ItemPositions {
        get {
            var itemPositions = new Dictionary<InventoryItem, List<Vector2>>();
            foreach(var itemSlot in this.ItemSlots) {
                if(itemSlot.Value == null) {
                    continue;
                }

                if(itemPositions.ContainsKey(itemSlot.Value)) {
                    itemPositions[itemSlot.Value].Add(itemSlot.Key);
                }
                else {
                    itemPositions.Add(itemSlot.Value, new List<Vector2>() { itemSlot.Key });
                }
            }
            return itemPositions;
        }
    }
    
    public event Action InventoryChanged;
    public event Action<InventoryItem> ItemAdded;
    public event Action<InventoryItem> ItemRemoved;
    
    
    public float Weight {
        get {
            float weight = 0;
            if(this.ItemPositions == null){
                return 0;
            }
            
            foreach(var inventoryItem in this.ItemPositions.Keys) {
                weight += (inventoryItem.Item.Config.Weight * inventoryItem.Quantity);
            }
            return weight;
        }
    }

    

    public Inventory(InventoryConfig config) {
        this.Config = config;

        this.CreateItemSlots();
    }


    private InventoryItem MapTonventoryItem(Item item) {
        return new InventoryItem() {
            Item = item,
            Quantity = 1
        };
    }

    public void CreateItemSlots() {
        this.ItemSlots = new Dictionary<Vector2, InventoryItem>();
        
        for(int x = 0; x < this.Config.SlotsX; x++) {
            for(int y = 0; y < this.Config.SlotsY; y++) {
                this.ItemSlots.Add(new Vector2(x, y), null);
            }
        }
    }

    public bool CanFitItemAt(Item item, Vector2 position) {
        int slotsX = item.Config.Inventory.SlotsX / this.Config.SlotsX;
        int slotsY = item.Config.Inventory.SlotsY / this.Config.SlotsY;

        if(slotsX * slotsY != item.Config.Inventory.SlotsX * item.Config.Inventory.SlotsY) {
            return false;
        }

        for(int x = 0; x < slotsX; x++) {
            for(int y = 0; y < slotsY; y++) {
                if(this.ItemSlots[new Vector2(position.x + x, position.y + y)] != null) {
                    return false;
                }
            }
        }

        return true;
    }
    
    public void GetFirstFittingSlot(Item item, out Vector2 position) {
        position = new Vector2(-1, -1);
        for(int x = 0; x < this.Config.SlotsX; x++) {
            for(int y = 0; y < this.Config.SlotsY; y++) {
                if(this.CanFitItemAt(item, new Vector2(x, y))) {
                    position = new Vector2(x, y);
                    return;
                }
            }
        }
    }
    
    public bool TryAddItem(Item item, Vector2 position) {
        //@TODO: noch einbauen, dass wenn ein leerer Stack gefunden wird, dass der Stack dann gefüllt wird
        if(!this.CanFitItemAt(item, position)) {
            return false;
        }
        var inventoryItem = InventoryItem.Create(item, 1);
        int slotsX = item.Config.Inventory.SlotsX / this.Config.SlotsX;
        int slotsY = item.Config.Inventory.SlotsY / this.Config.SlotsY;

        for(int x = 0; x < slotsX; x++) {
            for(int y = 0; y < slotsY; y++) {
                this.ItemSlots[new Vector2(position.x + x, position.y + y)] = inventoryItem;
    }
        }
        this.InventoryChanged?.Invoke();
        this.ItemAdded?.Invoke(inventoryItem);
        return true;
    }

    public void RemoveItem(InventoryItem inventoryItem) {
        bool foundItem = false;

        for(int x = 0; x < this.Config.SlotsX; x++) {
            for(int y = 0; y < this.Config.SlotsY; y++) {
                if(this.ItemSlots[new Vector2(x, y)] == inventoryItem) {
                    this.ItemSlots[new Vector2(x, y)] = null;
                    foundItem = true;
                }
            }
        }

        if(foundItem) {
            this.InventoryChanged?.Invoke();
            this.ItemRemoved?.Invoke(inventoryItem);
        }
    }
}
