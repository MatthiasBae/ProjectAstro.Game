using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour {
	
	private static InventoryUIController _instance;
	public static InventoryUIController Instance {
		get {
			if(_instance == null){
				_instance = FindObjectOfType<InventoryUIController>();
			}

			return _instance;
		}
	}
	
	public InventoryItemUI SelectedInventoryItemUI;
	public GameObject SelectedInventoryItemUIContainer;
	
	public ItemContainerUIBase ItemPlacedContainerUI;
	
	public bool HasItemSelected{
		get => this.SelectedInventoryItemUI is not null;
	}

	public void Update(){
		var selectedItem = this.SelectedInventoryItemUI;
		if(selectedItem is not null){
			selectedItem.gameObject.transform.position = MouseHelper.MousePosition;
		}
	}
	
	public bool TrySelectItem(InventoryItemUI inventoryItemUI){
		if(this.SelectedInventoryItemUI is not null){
			return false;
		}
		this.SelectedInventoryItemUI = inventoryItemUI;
		this.SelectedInventoryItemUI.transform.SetParent(this.SelectedInventoryItemUIContainer.transform, false);
		return true;
	}
	
	public bool TryUnselectItem(){
		this.SelectedInventoryItemUI = null;
		return true;
	}
}
