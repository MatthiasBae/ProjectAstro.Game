using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
	public InventoryManager InventoryManager;
	public List<EquipmentSlot> EquipmentSlots;

	private void Awake(){
		this.InitializeEquipmentSlots();
		this.InitializeInventoryManager();
	}

	public void InitializeEquipmentSlots(){
		this.EquipmentSlots = new List<EquipmentSlot>();
	}
	
	public void InitializeInventoryManager(){
		this.InventoryManager = new InventoryManager();
		foreach (var equipmentSlot in this.EquipmentSlots){
			var item = equipmentSlot.GetItem();
			if (!item.Config.HasInventory){
				continue;
			}
			
			this.InventoryManager.AddInventory(equipmentSlot.Name, item.Inventory);
		}
	}
}
