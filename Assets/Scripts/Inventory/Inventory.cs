using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : ItemContainerBase {

    public float Weight{
        get => this.Slots.Values.Sum(item => item.Config.Weight * item.Amount);
    }
    public float MaxWeight{
        get => this.Config.MaxWeight;
    }

    public Inventory(InventoryConfig config) : base(config){
        
    }
    
    public override void AddItem(Item item, Vector2 slotPosition) {
        var endPosition = new Vector2{
            x = slotPosition.x + item.Config.SlotSize.x,
            y = slotPosition.y - item.Config.SlotSize.y
        };
        //Debug.Log(endPosition);
        for (var x = slotPosition.x; x < endPosition.x; x++){
            for (var y = slotPosition.y; y > endPosition.y; y--){
                var pos = new Vector2(x, y);
                this.Slots[pos]= item;
                //Debug.Log($"Adding to slot in inventory:{pos}");
            }
        }

        this.OnItemAdded(item, slotPosition);
    }

    public override void RemoveItem(Item item) {
        var slots = this.Slots.ToArray();
        var position = new Vector2{
            x = this.Config.Size.x - 1,
            y = -this.Config.Size.y + 1
        };
        
        var previousDistance = Vector2.Distance(position, Vector2.zero);
        var distance = Vector2.Distance(position, Vector2.zero);
        
        foreach(var slot in slots) {
            if(slot.Value == item) {
                this.Slots[slot.Key] = null;
                distance = Vector2.Distance(slot.Key, Vector2.zero);
                if(distance < previousDistance) {
                    position = slot.Key;
                }
                previousDistance = distance;
            }
            
        }
        //Debug.Log($"Item removed from Inventory:{position}");
        this.OnItemRemoved(item, position);
    }
}
