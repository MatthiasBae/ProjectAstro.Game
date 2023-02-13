using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EquipmentContainerUI : MonoBehaviour {
	[SerializeField]
	private ActorPartEquipment Equipment;

	[SerializeField]
	private InventoryUI SlotUI;
	[SerializeField] 
	private InventoryUI SlotItemInventoryUI;

	private void Start(){
		this.InitializeSlotInventory();
		this.RegisterEvents();
	}

	public void RegisterEvents(){
		this.SlotUI.ItemPlaced += this.SetItemInventory;
		this.SlotUI.ItemRemoved += this.SetItemInventory;
	}

	private void InitializeSlotInventory(){
		this.SlotUI.SetInventory(this.Equipment.Slot);
		this.SetItemInventory();
	}
	private void SetItemInventory(){
		
		var item = this.SlotUI.Inventory.Items.FirstOrDefault().Key;
		if (item is null && this.SlotItemInventoryUI is not null){
			this.SlotItemInventoryUI.SetInventory(null);
			return;
		}
		
		if(this.SlotItemInventoryUI is null){
			return;
		}
		
		var itemInventory = item.Inventory;
		if (itemInventory is null){
			this.SlotItemInventoryUI.SetInventory(null);
			return;
		} 
		this.SlotItemInventoryUI.SetInventory(item.Inventory);
	}
}
