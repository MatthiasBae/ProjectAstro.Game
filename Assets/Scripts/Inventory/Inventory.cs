using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : MonoBehaviour {

    public float Weight;
    public InventoryConfig Config;
    public List<InventorySlot> Slots;
    
    public event Action<InventorySlot> ItemAdded;
    public event Action<InventoryItem> ItemRemoved;

    private InventorySlot GetSlot(Vector2 position) {
        return this.Slots.FirstOrDefault(slot => slot.Position == position);
    }

    private void Awake() {
        this.CreateSlots();
    }

    public void CreateSlots() {
        this.Slots.Clear();
        for(int y = 0; y < this.Config.Size.y; y++) {
            for(int x = 0; x < this.Config.Size.x; x++) {
                var slot = new InventorySlot() {
                    Position = new Vector2(x, y),
                    InventoryItem = null
                };
            
                this.Slots.Add(slot);
            }
        }
    }
    
    public bool TryAddItem(Item item, int amount) {
        var addedItem = false;
        foreach(var slot in this.Slots) {
            if(slot.InventoryItem != null) {
                continue;
            }

            var addedItemAt = this.TryAddItemAt(item, amount, slot.Position);
            if(addedItemAt) {
                addedItem = true;
                break;
            }
        }
        return addedItem;
    }
    
    public bool TryAddItemAt(Item item, int amount, Vector2 position) {
        var hasEnoughSlots = this.HasEnoughSlots(item);
        if(!hasEnoughSlots) {
            return false;
        }
        
        var slotArea = this.GetSlotArea(position, item.Config.Inventory.Size);
        var isPlaceable = this.IsItemPlaceableAt(slotArea);
        if(!isPlaceable) {
            return false;
        }

        var endPosition = position + item.Config.Inventory.Size;
        if(endPosition.x > this.Config.Size.x || endPosition.y > this.Config.Size.y) {
            return false;
        }

        var inventoryItem = InventoryItem.Create(item, amount);

        foreach(var slot in slotArea) {
            slot.InventoryItem = inventoryItem;
        }

        var startSlot = this.GetSlot(position);

        this.ItemAdded?.Invoke(startSlot);
        return true;
    }

    public bool IsItemPlaceableAt(List<InventorySlot> inventorySlots) {
        foreach(var slot in inventorySlots) {
            if(slot.InventoryItem!= null) {
                return false;
            }
        }

        return true;
    }

    public bool HasEnoughSlots(Item item){
        var itemSize = item.Config.Inventory.Size;
        var inventorySize = this.Config.Size;

        if(itemSize.x > inventorySize.x || itemSize.y > inventorySize.y) {
            return false;
        }
        return true;
    }


    public InventoryItem GetInventoryItem(Item item) {
        var inventorySlot = this.Slots.FirstOrDefault(slot => slot.InventoryItem!=null && slot.InventoryItem.Item == item);
        if(inventorySlot == null) {
            return null;
        }
        var inventoryItem = inventorySlot.InventoryItem;
        return inventoryItem;

    }
            

     public bool RemoveItem(Item item) {
        var inventoryItem = this.GetInventoryItem(item);
        if(inventoryItem == null) {
            return false;
        }

        foreach(var slot in this.Slots) {
            if(slot.InventoryItem == inventoryItem) {
                Debug.Log("Set InventoryItem to null");
                slot.InventoryItem = null;
            }
        }
        this.ItemRemoved?.Invoke(inventoryItem);
        return true;
    }

    public List<InventorySlot> GetSlotArea(Vector2 startPos, Vector2 size) {
        var endPos = startPos + size;
        var list = this.Slots.FindAll(slot => 
            slot.Position.x >= startPos.x 
            && slot.Position.y >= startPos.y
            && slot.Position.x < endPos.x
            && slot.Position.y < endPos.y).ToList();
        return list;
    }
    
    public List<Vector2> GetItemSlotArea(InventoryItem inventoryItem) {
        var list = new List<Vector2>();
        foreach(var slot in this.Slots) {
            if(slot.InventoryItem == inventoryItem) {
                list.Add(slot.Position);
            }
        }
        return list;
    }

}
