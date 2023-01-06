using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridUI : MonoBehaviour {

    [SerializeField]
    private RectTransform GridContainerRectTransform;
    [SerializeField]
    private RectTransform SlotContainerRectTransform;
    [SerializeField]
    private RectTransform ItemContainerRectTransform;

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
    private Color ItemNotPlaceable;
    [SerializeField]
    private Color ItemPlaceable;
    
    [SerializeField]
    private List<InventorySlotUI> Slots;

    [SerializeField]
    private List<InventoryItemUI> Items;
    
    private void Awake() {
        this.Slots = new List<InventorySlotUI>();
        this.Items = new List<InventoryItemUI>();
    }

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
    
    public void AddItem(InventoryItem inventoryItem, Vector2 position) {
        InventoryItemUI itemUI = this.CreateItem(inventoryItem);
        var cellSize = this.GridLayout.cellSize;
        itemUI.UpdateUI();
        itemUI.SetSize(cellSize * inventoryItem.Item.Config.Inventory.Size);

        var transformPos = new Vector2(position.x * cellSize.x, -position.y * cellSize.y);
        itemUI.SetPosition(transformPos); 
        this.Items.Add(itemUI);
    }
    public void AddItems(Inventory inventory) {
        foreach(var slot in inventory.Slots) {
            if(slot.InventoryItem == null) {
                continue;
            }

            this.AddItem(slot.InventoryItem, slot.Position);
        }
    }
    
    public void RemoveItem(InventoryItem inventoryItem) {
        var inventoryItemUI = this.Items.FirstOrDefault(item => item.InventoryItem == inventoryItem);
        this.Items.Remove(inventoryItemUI);
        Destroy(inventoryItemUI.gameObject);
    }

    private InventoryItemUI CreateItem(InventoryItem inventoryItem) {
        GameObject item = Instantiate(this.InventoryGridItemPrefab, this.ItemContainer.transform);
        InventoryItemUI itemUI = item.GetComponent<InventoryItemUI>();
        itemUI.InventoryItem = inventoryItem;
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
}
