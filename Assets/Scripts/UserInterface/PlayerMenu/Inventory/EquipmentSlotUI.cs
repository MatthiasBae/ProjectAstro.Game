using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : ItemContainerUIBase {
	
	public override void PlaceItem(InventoryItemUI inventoryItemUI){

		var unselected = InventoryUIController.Instance.TryUnselectItem();
		if (!unselected){
			return;
		}
		
		var anchoredPosition = this.CalculateAnchoredItemPosition(inventoryItemUI.Size, this.RectTransform.sizeDelta);
		inventoryItemUI.transform.SetParent(this.RectTransform, false);
		inventoryItemUI.SetPosition(anchoredPosition);
		this.AddItem(inventoryItemUI, Vector2.zero);
	}

	public override void PlaceItem(InventoryItemUI inventoryItemUI, Vector2 slotPosition){
		var unselected = InventoryUIController.Instance.TryUnselectItem();
		if (!unselected){
			return;
		}

		var anchoredPosition = this.CalculateAnchoredItemPosition(inventoryItemUI.Size, this.RectTransform.sizeDelta);
		inventoryItemUI.transform.SetParent(this.RectTransform, false);
		inventoryItemUI.SetPosition(anchoredPosition);
		this.AddItem(inventoryItemUI, slotPosition);
	}

	public override void AddItem(InventoryItemUI inventoryItemUI, Vector2 slotPosition){
		var slots = this.Slots.ToArray();
		foreach (var slot in slots){
			this.Slots[slot.Key] = inventoryItemUI;
		}
	}

	public override void RemoveItem(InventoryItemUI inventoryItemUI){
		base.RemoveItem(inventoryItemUI);
		
		InventoryUIController.Instance.TrySelectItem(inventoryItemUI);
	}
	
	public override Vector2 CalculateItemSize(Vector2 itemSlotCount, Vector2 gridSlotSize, Vector2 equipmentSlotSize){
		throw new NotImplementedException();
	}

	public override Vector2 CalculateAnchoredItemPosition(Vector2 itemSize, Vector2 equipmentSlotSize){
			var halfItemSize = itemSize / 2;
			var halfSlotSize = equipmentSlotSize / 2;
			var offset = new Vector2{
				x = halfSlotSize.x - halfItemSize.x,
				y = -(halfSlotSize.y - halfItemSize.y)
			};
			
			return offset;
	}

	public override void OnPointerClick(PointerEventData eventData){
		var inventoryUIController = InventoryUIController.Instance;
		var localPosition = UserInterfaceHelper.MousePositionToRectTransformPosition(MouseHelper.MousePosition ,this.RectTransform);
		var slotPosition = this.AnchoredPositionToSlotPosition(localPosition, this.SlotSize);
		Debug.Log(slotPosition);
		if (!inventoryUIController.HasItemSelected){
			var selectedItemUI = this.GetItem(slotPosition);
			this.OnItemSelected(selectedItemUI);
			return;
				
		}
		
		var inventoryItemUI = inventoryUIController.SelectedInventoryItemUI;
		this.OnItemDropped(inventoryItemUI, slotPosition);
	}
}
