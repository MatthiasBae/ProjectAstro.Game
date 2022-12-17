using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridUI : MonoBehaviour {

    [SerializeField]
    private RectTransform GridRectTransform;
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

    private Dictionary<Vector2, InventoryGridSlotUI> Slots = new Dictionary<Vector2, InventoryGridSlotUI>();
    private Dictionary<InventoryGridItemUI, List<Vector2>> Items = new Dictionary<InventoryGridItemUI, List<Vector2>>();
    
    public void InitializeSlots(Inventory inventory) {
        this.AddSlots(inventory.Config);
        this.UpdateSize(inventory.Config);
        this.UpdatePosition();
    }

    public void AddSlots(InventoryConfig inventoryConfig) {
        for(int x = 0; x < inventoryConfig.SlotsX; x++) {
            for(int y = 0; y < inventoryConfig.SlotsY; y++) {
                GameObject slot = this.CreateSlot();
                InventoryGridSlotUI slotUI = slot.GetComponent<InventoryGridSlotUI>();
                this.Slots.Add(new Vector2(x, y), slotUI);
            }
        }
    }

    public void UpdateSize(InventoryConfig inventoryConfig) {
        var cellSize = this.GridLayout.cellSize;
        var size = new Vector2(inventoryConfig.SlotsX * cellSize.x, inventoryConfig.SlotsY * cellSize.y);
        this.GridRectTransform.sizeDelta = size;
        this.ItemContainerRectTransform.sizeDelta = size;
    }

    public void UpdatePosition() {
        var pos = new Vector2(this.GridRectTransform.sizeDelta.x / 2, -(this.GridRectTransform.sizeDelta.y / 2));
        this.GridRectTransform.anchoredPosition = pos;
        this.ItemContainerRectTransform.anchoredPosition = pos;
    }

    private GameObject CreateSlot() {
        GameObject slot = Instantiate(this.InventoryGridSlotPrefab, this.SlotContainer.transform);
        return slot;
    }

    public void ClearSlots() {
        foreach(var slot in this.Slots.Values) {
            Destroy(slot.gameObject);
        }
        this.Slots.Clear();
    }

    public void AddItems(Inventory inventory) {
        foreach(var inventoryItem in inventory.ItemPositions) {
            if(inventoryItem.Value != null) {
                this.AddItem(inventoryItem.Key, inventoryItem.Value);
            }
        }
    }

    public void AddItem(InventoryItem inventoryItem, List<Vector2> coordinates) {
        InventoryGridItemUI itemUI = this.CreateItem(inventoryItem);
        this.Items.Add(itemUI, coordinates);
    }

    private InventoryGridItemUI CreateItem(InventoryItem inventoryItem) {
        GameObject item = Instantiate(this.InventoryGridItemPrefab, this.ItemContainer.transform);
        InventoryGridItemUI itemUI = item.GetComponent<InventoryGridItemUI>();
        itemUI.InventoryItem = inventoryItem;
        return itemUI;
    }

    public void ClearItems() {
        foreach(var item in this.Items.Keys) {
            Destroy(item.gameObject);
        }
        this.Items.Clear();
    }

    public void ClearItem(InventoryGridItemUI itemUI) {
        Destroy(itemUI.gameObject);
        this.Items.Remove(itemUI);
    }
}
