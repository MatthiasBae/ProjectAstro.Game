using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGridUI : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    private InventoryDragDropUIController DragDropController;

    public RectTransform GridContainerRectTransform;
    public RectTransform SlotContainerRectTransform;
    public RectTransform ItemContainerRectTransform;

    [SerializeField]
    private GameObject InventoryGridSlotPrefab;
    [SerializeField]
    private GameObject InventoryGridItemPrefab;

    [SerializeField]
    private GameObject SlotContainer;
    [SerializeField]
    private GameObject ItemContainer;
    
    [SerializeField]
    private GridLayoutGroup GridLayout;
    
    [SerializeField]
    private List<InventorySlotUI> Slots;
    [SerializeField]
    private List<InventoryItemUI> Items;

    public event Action<KeyValuePair<InventoryItemUI, Vector2>> ItemDropped;

    public void InitializeSlots(Inventory inventory) {
        this.AddSlots(inventory.Config);
        this.UpdateSize(inventory.Config);
        this.UpdatePosition();
    }

    public void AddSlots(InventoryConfig inventoryConfig) {
        for(int x = 0; x < inventoryConfig.Size.x; x++) {
            for(int y = 0; y < inventoryConfig.Size.y; y++) {
                GameObject slot = this.CreateSlot();
                InventorySlotUI slotUI = slot.GetComponent<InventorySlotUI>();
                this.Slots.Add(slotUI);
            }
        }
    }

    public Vector2 LocalPositionToSlotPosition(Vector2 localPosition) {
        var slotPosition = new Vector2 {
            x = Mathf.Floor(localPosition.x / this.GridLayout.cellSize.x),
            y = Mathf.Floor(localPosition.y / this.GridLayout.cellSize.y)
        };
        return slotPosition;
    }


    public void UpdateSize(InventoryConfig inventoryConfig) {
        var cellSize = this.GridLayout.cellSize;
        var gridSize = new Vector2(inventoryConfig.Size.x * cellSize.x, inventoryConfig.Size.y * cellSize.y);
        var gridContainerSize = new Vector2(gridSize.x, gridSize.y + (40*2));
        this.SlotContainerRectTransform.sizeDelta = gridSize;
        this.ItemContainerRectTransform.sizeDelta = gridSize;
        this.GridContainerRectTransform.sizeDelta = gridContainerSize;
    }

    public void UpdatePosition() {
        var pos = new Vector2(this.SlotContainerRectTransform.sizeDelta.x / 2, -(this.SlotContainerRectTransform.sizeDelta.y / 2));
        this.SlotContainerRectTransform.anchoredPosition = pos;
        this.ItemContainerRectTransform.anchoredPosition = pos;
    }

    private GameObject CreateSlot() {
        GameObject slot = Instantiate(this.InventoryGridSlotPrefab, this.SlotContainer.transform);
        return slot;
    }

    public void ClearSlots() {
        foreach(var slot in this.Slots) {
            Destroy(slot.gameObject);
        }
        this.Slots.Clear();
    }

    public void AddItem(InventoryItem inventoryItem, Vector2 position, InventoryDragDropUIController dragDropController, InventoryUI inventoryUI) {
        InventoryItemUI itemUI = this.CreateItem(inventoryItem, dragDropController, inventoryUI);
        var cellSize = this.GridLayout.cellSize;
        itemUI.UpdateUI();
        itemUI.SetSize(cellSize * inventoryItem.Item.Config.Inventory.Size);

        var transformPos = new Vector2(position.x * cellSize.x, -position.y * cellSize.y);
        itemUI.SetAnchoredPosition(transformPos); 
        this.Items.Add(itemUI);
    }
    public void AddItem(InventoryItemUI itemUI, Vector2 position) {
        var cellSize = this.GridLayout.cellSize;
        var transformPos = new Vector2(position.x * cellSize.x, -position.y * cellSize.y);
        itemUI.gameObject.transform.SetParent(this.ItemContainer.transform);
        itemUI.SetAnchoredPosition(transformPos);
        this.Items.Add(itemUI);
    }
    public void AddItems(InventoryUI inventoryUI, InventoryDragDropUIController dragDropController) {
        var inventory = inventoryUI.Inventory;
        foreach(var slot in inventory.Slots) {
            if(slot.InventoryItem == null) {
                continue;
            }

            this.AddItem(slot.InventoryItem, slot.Position, dragDropController, inventoryUI);
        }
    }
    
    public void RemoveItem(InventoryItem inventoryItem, bool destroyItemUI = true) {
        var inventoryItemUI = this.Items.FirstOrDefault(item => item.InventoryItem == inventoryItem);
        this.Items.Remove(inventoryItemUI);
        if(!destroyItemUI) {
            return;
        }
        
        Destroy(inventoryItemUI.gameObject);
    }

    private InventoryItemUI CreateItem(InventoryItem inventoryItem, InventoryDragDropUIController dragDropController, InventoryUI inventoryUI) {
        var item = Instantiate(this.InventoryGridItemPrefab, this.ItemContainer.transform);
        var itemUI = item.GetComponent<InventoryItemUI>();
        itemUI.DragDropController = dragDropController;
        itemUI.InventoryItem = inventoryItem;
        itemUI.InventoryUI = inventoryUI;
        return itemUI;
    }

    public void ClearItems() {
        foreach(var item in this.Items) {
            Destroy(item.gameObject);
        }
        this.Items.Clear();
    }

    public void ClearItem(InventoryItemUI itemUI) {
        Destroy(itemUI.gameObject);
        this.Items.Remove(itemUI);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(this.DragDropController.SelectedUIItem == null) {
            return;
        }
        this.DragDropController.SelectGrid(this);
    }

    public void OnItemDropped(InventoryItemUI itemUI, Vector2 position) {
        this.ItemDropped?.Invoke(new KeyValuePair<InventoryItemUI, Vector2>(itemUI, position));
    }
}
