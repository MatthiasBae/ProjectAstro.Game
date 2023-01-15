using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : MonoBehaviour {

    public float Weight;
    
    public InventoryConfig Config;
    public List<InventorySlot> Slots;
    
    public event Action<InventoryItem, Vector2, bool> ItemAddedTry;
    public event Action<InventoryItem, Vector2, bool> ItemRemovedTry;
    
    public event Action<InventoryItem, Vector2> ItemAdded;
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

    public bool TryAddItem(InventoryItem inventoryItem) {
        var addedItem = false;
        foreach(var slot in this.Slots) {
            if(slot.InventoryItem != null) {
                continue;
            }

            var addedItemAt = this.TryAddItemAt(inventoryItem, slot.Position);
            if(addedItemAt) {
                Debug.Log($"Füge hinzu InventoryItem in {slot.Position} in Inventar {this.Config.Name}");
                addedItem = true;
                break;
            }
        }
        return addedItem;
    }

    public bool TryAddItemAt(InventoryItem inventoryItem, Vector2 position) {
        var item = inventoryItem.Item;
        var startSlot = this.GetSlot(position);
        var hasEnoughSlots = this.HasEnoughSlots(item);
        
        if(!hasEnoughSlots) {
            this.ItemAddedTry?.Invoke(inventoryItem, position, false);
            return false;
        }

        var endPosition = position + item.Config.Inventory.Size;
        if(endPosition.x > this.Config.Size.x || endPosition.y > this.Config.Size.y) {
            this.ItemAddedTry?.Invoke(inventoryItem, position, false);
            return false;
        }

        var slotArea = this.GetSlotArea(position, item.Config.Inventory.Size);
        var isPlaceable = this.IsItemPlaceableAt(slotArea);
        if(!isPlaceable) {
            this.ItemAddedTry?.Invoke(inventoryItem, position, false);
            return false;
        }

        foreach(var slot in slotArea) {
            slot.InventoryItem = inventoryItem;
        }
        
        this.Weight += inventoryItem.Weight;
        
        //Debug.Log($"Füge hinzu InventoryItem in {startSlot} in Inventar {this.Config.Name}");
        this.ItemAddedTry?.Invoke(inventoryItem, position, true);
        this.ItemAdded?.Invoke(inventoryItem, position);
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

    public bool RemoveItem(InventoryItem inventoryItem) {
        if(inventoryItem == null) {
            return false;
        }

        foreach(var slot in this.Slots) {
            if(slot.InventoryItem == inventoryItem) {
                //Debug.Log($"Entferne InventoryItem in {slot.Position} aus Inventar {this.Config.Name}");
                slot.InventoryItem = null;
            }
        }
        this.Weight -= inventoryItem.Weight;
        this.ItemRemoved?.Invoke(inventoryItem);
        return true;
    }

    public bool ItemExists(InventoryItem inventoryItem) {
        foreach(var slot in this.Slots) {
            if(slot.InventoryItem == inventoryItem) {
                return true;
            }
        }
        return false;
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
