using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public InventoryConfig Config;
    public Dictionary<Vector2, Item> ItemSlots;

    public event Action InventoryChanged;
    
    public Inventory(InventoryConfig config) {
        this.Config = config;

        this.CreateItemSlots();
    }

    private void CreateItemSlots() {
        this.ItemSlots = new Dictionary<Vector2, Item>();
        
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
    public bool TryAddItem(Item item, Vector2 position) {

        if(!this.CanFitItemAt(item, position)) {
            return false;
        }

        int slotsX = item.Config.Inventory.SlotsX / this.Config.SlotsX;
        int slotsY = item.Config.Inventory.SlotsY / this.Config.SlotsY;

        for(int x = 0; x < slotsX; x++) {
            for(int y = 0; y < slotsY; y++) {
                this.ItemSlots[new Vector2(position.x + x, position.y + y)] = item;
            }
        }
        this.InventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(Item item) {
        bool foundItem = false;

        for(int x = 0; x < this.Config.SlotsX; x++) {
            for(int y = 0; y < this.Config.SlotsY; y++) {
                if(this.ItemSlots[new Vector2(x, y)] == item) {
                    this.ItemSlots[new Vector2(x, y)] = null;
                    foundItem = true;
                }
            }
        }

        if(foundItem) {
            this.InventoryChanged?.Invoke();
        }
    }
}
