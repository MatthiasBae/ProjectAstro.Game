using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentSlot : ItemContainerBase {
	public EquipmentSlot(InventoryConfig config) : base(config){ }
	public override void AddItem(Item item, Vector2 position){
		var slots = this.Slots.ToArray();
		foreach(var slot in slots){
			this.Slots[slot.Key] = item;
		}
		this.OnItemAdded(item, position);
	}

	public override void RemoveItem(Item item){
		var slots = this.Slots.ToArray();
		foreach(var slot in slots){
			this.Slots[slot.Key] = null;
		}
		this.OnItemRemoved(item, Vector2.zero);
	}
	
	public Item GetItem(){
		var slots = this.Slots.ToArray();
		foreach(var slot in slots){
			if(slot.Value != null){
				return slot.Value;
			}
		}
		return null;
	}
	
	public override bool TryAddItemAt(Item item, Vector2 position){
		if(this.Slots[position] != null){
			return false;
		};
		
		var itemTooLarge = this.IsItemTooLarge(item);
		if(itemTooLarge) {
			return false;
		}
        
		//Calculate the endposition if the y axis on the grid is negative

		//Hier wird neues Item hinzugefügt
		this.AddItem(item, position);
		return true;
	}
}
