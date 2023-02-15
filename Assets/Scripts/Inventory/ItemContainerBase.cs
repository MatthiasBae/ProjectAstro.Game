using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ItemContainerBase {
	public string Name{
		get => this.Config.Name;
	}
	public InventoryConfig Config;
	public Dictionary<Vector2, Item> Slots;
	public Dictionary<Item, Vector2> Items {
		get => this.Slots
			.Where(kvp => kvp.Value != null)
			.GroupBy(kvp => kvp.Value)
			.ToDictionary(group => group.Key, group => group.First().Key);
	}
	
	public event Action<Item, Vector2> ItemRemoved;
	public event Action<Item, Vector2> ItemAdded;
	
	public ItemContainerBase(InventoryConfig config){
		this.Config = config;
		this.Slots = new Dictionary<Vector2, Item>();
		this.CreateSlots();
	}
	
	public void CreateSlots(){
		for (int y = 0; y > -this.Config.Size.y; y--){
			for (int x = 0; x < this.Config.Size.x; x++){
				this.Slots.Add(
					new Vector2(x, y),
					null
				);
			}
		}
	}
	
	public bool TryAddItem(Item item){
		var added = false;
		var itemTooLarge = this.IsItemTooLarge(item);
		if(itemTooLarge) {
			return false;
		}
        
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

	public bool TryRemoveItem(Item item){
		this.RemoveItem(item);
		return true;
	}
	
	public abstract void AddItem(Item item, Vector2 position);
	public abstract void RemoveItem(Item item);

	public virtual bool TryAddItemAt(Item item, Vector2 position){
		if(this.Slots[position] != null) {
			return false;
		}
		
		var itemTooLarge = this.IsItemTooLarge(item);
		if(itemTooLarge) {
			return false;
		}
        
		//Calculate the endposition if the y axis on the grid is negative

		var endPosition = new Vector2{
			x = (item.Config.Slots.x + position.x),
			y = -(item.Config.Slots.y - position.y)
		};
        
		if(endPosition.x > this.Config.Size.x || endPosition.y < -this.Config.Size.y) {
			return false;
		}
		
		//Debug.Log($"EndPosition: {endPosition} | Position: {position} | SlotSize: {item.Config.SlotSize}");
		var isSlotAreaEmpty = this.IsSlotAreaEmpty(position, endPosition);
		if(!isSlotAreaEmpty) {
			return false;
		}

		//Hier wird neues Item hinzugefügt
		this.AddItem(item, position);
		return true;
	}
	
	
	public bool IsSlotAreaEmpty(Vector2 startPos, Vector2 endPos) {
		var isSlotAreaEmpty = true;
		for(int y = (int)startPos.y; y > endPos.y; y--) {
			for(int x = (int)startPos.x; x < endPos.x; x++) {
				var slotPos = new Vector2(x, y);
				var slotItem = this.Slots[slotPos];
				if(slotItem != null) {
					isSlotAreaEmpty = false;
					break;
				}
			}
		}
		return isSlotAreaEmpty;
	}

	public bool IsItemTooLarge(Item item) {
		var itemSlotCount = item.Config.Slots.x * item.Config.Slots.y;
		var inventorySlotCount = this.Config.Size.x * this.Config.Size.y;

		if(itemSlotCount > inventorySlotCount) {
			return true;
		}
        
		if(item.Config.Slots.x > this.Config.Size.x || item.Config.Slots.y > this.Config.Size.y) {
			return true;
		}
        
		return false;
	}
	
	
	public bool TryStackItem(Item inventoryItem, Item item) {
		var maxAmount = inventoryItem.Config.MaxStackSize;
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

	public void OnItemRemoved(Item item, Vector2 position) {
		this.ItemRemoved?.Invoke(item, position);
	}
    
	public void OnItemAdded(Item item, Vector2 position) {
		this.ItemAdded?.Invoke(item, position);
	}
}
