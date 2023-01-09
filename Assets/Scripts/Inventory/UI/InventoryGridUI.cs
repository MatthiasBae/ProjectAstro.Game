using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
    private List<InventoryItemUI> Items;
    [SerializeField]
    private List<InventorySlotUI> Slots;


    public event Action<InventoryItemUI, Vector2> ItemDropped;
    public event Action<InventoryItemUI> ItemSelected;


    public void Render(Vector2 size) {
        this.AddSlots(size);
        this.UpdateSize(size);
        this.UpdatePosition();
    }
    
    public void AddSlots(Vector2 size) {
        for(int x = 0; x < size.x; x++) {
            for(int y = 0; y < size.y; y++) {
                this.AddSlot(this.InventoryGridSlotPrefab);
            }
        }
    }

    private void AddSlot(GameObject gameObject) {
        GameObject slot = Instantiate(gameObject, this.SlotContainer.transform);
        var slotUI = slot.GetComponent<InventorySlotUI>();
        this.Slots.Add(slotUI);
    }

    public void DestroySlots() {
        foreach(var slot in this.Slots) {
            GameObject.Destroy(slot.gameObject);
        }
        this.Slots.Clear();
    }
    
    public Vector2 LocalPositionToSlotPosition(Vector2 localPosition) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.SlotContainerRectTransform, localPosition, null, out localPosition);
        var cellSize = this.GridLayout.cellSize;
        
        localPosition.y = -localPosition.y;
        localPosition += this.SlotContainerRectTransform.sizeDelta / 2;

        var slotPosition = new Vector2 {
            x = Mathf.Floor(localPosition.x / cellSize.x),
            y = Mathf.Floor(localPosition.y / cellSize.y)
        };
        return slotPosition;
    }

    public Vector2 SlotPositionToAnchoredPosition(Vector2 slotPosition) {
        var cellSize = this.GridLayout.cellSize;
        var anchoredPosition = new Vector2 {
            x = slotPosition.x * cellSize.x,
            y = -(slotPosition.y * cellSize.y)
        };
        return anchoredPosition;
    }

    public void UpdateSize(Vector2 inventorySize) {
        var cellSize = this.GridLayout.cellSize;
        var size = new Vector2 {
            x = inventorySize.x * cellSize.x,
            y = inventorySize.y * cellSize.y
        };
        
        this.SetSize(size);
    }

    public void UpdatePosition() {
        var containerSize = this.GridContainerRectTransform.sizeDelta;
        var position = new Vector2(containerSize.x / 2, -containerSize.y / 2);
        
        this.SetPosition(position);
    }

    public void SetPosition(Vector2 position) {
        this.GridContainerRectTransform.anchoredPosition = position;
        this.SlotContainerRectTransform.anchoredPosition = position;
        this.ItemContainerRectTransform.anchoredPosition = position;

    }

    public void SetSize(Vector2 size) {
        this.SlotContainerRectTransform.sizeDelta = size;
        this.ItemContainerRectTransform.sizeDelta = size;
        this.GridContainerRectTransform.sizeDelta = size;
        
        //gridContainerSize etwas größer gemacht damit es visuell ein bisschen schöner ist
        //var gridContainerSize = new Vector2(gridSize.x, gridSize.y + (40*2));
    }

    //Generiert ein neues UI Gameobject für das Item
    public void AddItem(InventoryItem inventoryItem, Vector2 slotCoordinates, InventoryDragDropUIController dragDropController, InventoryUI inventoryUI) {
        
        var cellSize = this.GridLayout.cellSize;
        var itemSize = inventoryItem.Item.Config.Inventory.Size;
        var itemUISize = cellSize * itemSize;
        
        var itemUI = this.CreateItemUI(inventoryItem, dragDropController, inventoryUI, itemUISize);
        this.SetItemPosition(itemUI, slotCoordinates);

        this.Items.Add(itemUI);
    }
    //Generiert KEIN neues UI Gameobject für das Item und nimmt das vorhandene
    public void AddItem(InventoryItemUI inventoryItemUI, Vector2 slotCoordinates, InventoryUI inventoryUI) {
        this.SetItemPosition(inventoryItemUI, slotCoordinates);
        inventoryItemUI.InventoryUI = inventoryUI;
        this.Items.Add(inventoryItemUI);
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


    private void SetItemPosition(InventoryItemUI itemUI, Vector2 slotCoordinates) {
        itemUI.transform.SetParent(this.ItemContainer.transform);

        var anchoredPosition = this.SlotPositionToAnchoredPosition(slotCoordinates);
        itemUI.SetAnchoredPosition(anchoredPosition);
    }

    public void RemoveItem(InventoryItem inventoryItem, bool destroyItemUI = true) {
        var inventoryItemUI = this.Items.FirstOrDefault(item => item.InventoryItem == inventoryItem);
        this.Items.Remove(inventoryItemUI);
        if(!destroyItemUI) {
            return;
        }
        
        Destroy(inventoryItemUI.gameObject);
    }

    private InventoryItemUI CreateItemUI(InventoryItem inventoryItem, InventoryDragDropUIController dragDropController, InventoryUI inventoryUI, Vector2 size) {
        var itemPrefab = this.InventoryGridItemPrefab;
        var itemContainerTransform = this.ItemContainer.transform;

        var item = Instantiate(itemPrefab, itemContainerTransform);
        var itemUI = item.GetComponent<InventoryItemUI>();
        itemUI.DragDropController = dragDropController;
        itemUI.InventoryItem = inventoryItem;
        itemUI.InventoryUI = inventoryUI;
        itemUI.UpdateUI(size);
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
            Debug.Log("Ausgewähltes item ist hier null. Daher wird nix beim Klick gemacht");
            return;
        }
        var inventoryUIItem = this.DragDropController.SelectedUIItem;
        var coordinates = this.LocalPositionToSlotPosition(Input.mousePosition);


        this.OnItemDropped(inventoryUIItem, coordinates);
    }

    public void OnItemDropped(InventoryItemUI inventoryUIItem, Vector2 position) {
        this.ItemDropped?.Invoke(inventoryUIItem, position);
    }

    public void OnItemSelected(InventoryItemUI inventoryItemUI) {
        this.ItemSelected?.Invoke(inventoryItemUI);
    }
}
