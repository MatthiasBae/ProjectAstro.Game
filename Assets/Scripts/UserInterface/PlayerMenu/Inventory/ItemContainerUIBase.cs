using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemContainerUIBase : MonoBehaviour, IPointerClickHandler {
	public RectTransform RectTransform;
	
	public event Action<InventoryItemUI> ItemAdded;
	public event Action<InventoryItemUI> ItemRemoved;

	public event Action<InventoryItemUI, Vector2> ItemDropped;
	public event Action<InventoryItemUI> ItemSelected;

	public Dictionary<Vector2, InventoryItemUI> Slots;
	public Vector2 SlotSize {
		get => InventoryUIController.Instance.PixelPerSlot;
	}
	
	
	public abstract void PlaceItem(InventoryItemUI inventoryItemUI);
	public abstract void PlaceItem(InventoryItemUI inventoryItemUI, Vector2 slotPosition);
	
	public abstract void AddItem(InventoryItemUI inventoryItemUI, Vector2 slotPosition);
	public virtual void RemoveItem(InventoryItemUI inventoryItemUI){
		var slots = this.Slots.ToArray();
		foreach(var item in slots){
			if (item.Value == inventoryItemUI){
				this.Slots[item.Key] = null;
				//Debug.Log($"Cleaning slot:{item.Key}");
			}
		}
	}
	public void DestroyItem(InventoryItemUI inventoryItemUI){
		this.RemoveItem(inventoryItemUI);
		Destroy(inventoryItemUI.gameObject);
	}

	public InventoryItemUI GetItem(Vector2 slotPosition){
		return this.Slots[slotPosition];
	}
	
	public void CreateSlots(Vector2 slots){
		this.Slots = new Dictionary<Vector2, InventoryItemUI>();
		for (int y = 0; y > -slots.y; y--){
			for (int x = 0; x < slots.x; x++){
				this.Slots.Add(
					new Vector2(x, y),
					null
				);
			}
		}
	}

	public void Reset(){
		this.Slots.Clear();
	}
	public void ResetUI(){
		var items = this.RectTransform.GetComponentsInChildren<InventoryItemUI>();
		foreach(var child in items){
			Destroy(child.gameObject);
		}
	}
	public abstract Vector2 CalculateItemSize(Vector2 itemSlotCount, Vector2 gridSlotSize , Vector2 equipmentSlotSize);
	public abstract Vector2 CalculateAnchoredItemPosition(Vector2 itemSize, Vector2 equipmentSlotSize);
	public Vector2 AnchoredPositionToSlotPosition(Vector2 anchoredPosition, Vector2 slotSize) {
		return new Vector2{
			x = Mathf.Floor(anchoredPosition.x / slotSize.x),
			y = Mathf.Ceil(anchoredPosition.y / slotSize.y)
		};
	}

	public void OnItemDropped(InventoryItemUI inventoryItemUI, Vector2 slotPosition){
		this.ItemDropped?.Invoke(inventoryItemUI, slotPosition);
	}
	
	public void OnItemSelected(InventoryItemUI inventoryItemUI){
		this.ItemSelected?.Invoke(inventoryItemUI);
	}
	
	public void OnItemAdded(InventoryItemUI inventoryItemUI){
		this.ItemAdded?.Invoke(inventoryItemUI);
	}
	
	public void OnItemRemoved(InventoryItemUI inventoryItemUI){
		this.ItemRemoved?.Invoke(inventoryItemUI);
	}
	
	public abstract void OnPointerClick(PointerEventData eventData);
}
