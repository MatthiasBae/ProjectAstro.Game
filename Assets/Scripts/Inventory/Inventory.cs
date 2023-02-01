using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public Dictionary<Vector2, Item> Slots;

    public InventoryConfig Config;

    public event Action<Item> ItemRemoved;
    public event Action<Item, Vector2> ItemAdded;
    public event Action<Item, Vector2, bool> ItemAddedTry;

    public Inventory(InventoryConfig inventoryConfig) {
        this.Config = inventoryConfig;
        this.Slots = new Dictionary<Vector2, Item>();
        this.CreateSlots();
    }

    private void CreateSlots() {
        for(int y = 0; y < this.Config.Size.y; y++) {
            for(int x = 0; x < this.Config.Size.x; x++) {
                this.Slots.Add(
                    new Vector2(x, y),
                    null
                );
            }
        }
    }

    public bool TryAddItem(Item item) {
        var added = false;
        foreach(var slot in this.Slots) {
            var slotPos = slot.Key;
            var slotItem = slot.Value;
            if(slotItem != null) {
                continue;
            }

            added = this.TryAddItemAt(item, slotPos);
            
            if(added) {
                break;
            }
        }
        return added;
    }

    public bool TryAddItemAt(Item item, Vector2 position) {
        var itemTooLarge = this.IsItemTooLarge(item);
        if(itemTooLarge) {
            this.OnItemAddedTry(item, position, false);
            return false;
        }

        var endPosition = item.Config.Inventory.Size + position;
        var isSlotAreaEmpty = this.IsSlotAreaEmpty(position, endPosition);
        if(!isSlotAreaEmpty) {
            this.OnItemAddedTry(item, position, false);
            return false;
        }

        //Hier wird neues Item hinzugefügt
        this.AddItem(item, position);
        this.OnItemAddedTry(item, position, true);
        return true;
    }

    public void AddItem(Item item, Vector2 position) {
        foreach(var slot in this.Slots) {
            var pos = slot.Key;
            this.Slots[pos] = item;
        }
        this.OnItemAdded(item, position);
    }

    public void RemoveItem(Item item) {
        foreach(var slot in this.Slots) {
            if(slot.Value == item) {
                this.Slots[slot.Key] = null;
            }
            
        }
        this.OnItemRemoved(item);
    }

    public bool TryStackItem(Item inventoryItem, Item item) {
        var maxAmount = inventoryItem.Config.Inventory.MaxStackSize;
        var stacked = false;
        for(int i = 0; i <= item.Amount; i++) {
            if(inventoryItem.Amount >= maxAmount) {
                break;
            }
            item.Amount--;
            inventoryItem.Amount++;
            stacked = true;
        }
        return stacked;
    }

    public bool IsSlotAreaEmpty(Vector2 startPos, Vector2 endPos) {
        for(int y = (int)startPos.y; y <= endPos.y; y++) {
            for(int x = (int)startPos.x; x <= endPos.x; x++) {
                var pos = new Vector2(x, y);
                var slotItem = this.Slots[pos];

                if(slotItem == null) {

                }
            }
        }

        return true;
    }

    public bool IsItemTooLarge(Item item) {
        var itemSlotCount = item.Config.Inventory.Size.x * item.Config.Inventory.Size.y;
        var inventorySlotCount = this.Config.Size.x * this.Config.Size.y;

        if(itemSlotCount > inventorySlotCount) {
            return true;
        }
        return false;
    }

    public void OnItemRemoved(Item item) {
        this.ItemRemoved?.Invoke(item);
    }

    public void OnItemAdded(Item item, Vector2 position) {
        this.ItemAdded?.Invoke(item, position);
    }

    public void OnItemAddedTry(Item item, Vector2 position, bool success) {
        this.ItemAddedTry?.Invoke(item, position, success);
    }
}
