using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour {

	[SerializeField] 
	private RectTransform ItemRect;
	
	[SerializeField] 
	private TMP_Text Amount;
	public Image Icon;
	
	public Item Item;

	public Vector2 Size {
		get => this.ItemRect.sizeDelta;
	}
	
	public static InventoryItemUI Create(GameObject prefab, Item item, Vector2 gridSlotSize, GameObject parent){
		var inventoryItemGameObject = GameObject.Instantiate(prefab, parent.transform);
		//Debug.Log(inventoryItemGameObject.transform.localScale);
		var inventoryItem = inventoryItemGameObject.GetComponent<InventoryItemUI>();
		inventoryItem.SetItem(item, gridSlotSize);
		return inventoryItem;
	}

	public void SetItem(Item item, Vector2 gridSlotSize){
		this.Item = item;
		this.UpdateVisuals();
		this.SetSize(gridSlotSize, this.Item.Config.SlotSize);
	}

	private void UpdateVisuals(){
		this.Amount.text = this.Item.Amount.ToString();
		this.Icon.sprite = this.Item.Config.SpriteUI;
		//Debug.Log(this.transform.localScale);
	}

	public void SetSize(Vector2 gridSlotSize, Vector2 slotCount){
		var size = new Vector2{
			x = slotCount.x * gridSlotSize.x,
			y = slotCount.y * gridSlotSize.y
		};
		this.ItemRect.sizeDelta = size;
	}

	public void SetPosition(Vector2 anchoredPosition){
		this.ItemRect.anchoredPosition = anchoredPosition;
	}
}
