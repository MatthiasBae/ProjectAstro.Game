using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotsUI : MonoBehaviour {
    public RectTransform RectTransform;
    public GridLayoutGroup GridLayoutGroup;
    
    public Dictionary<Vector2Int, GameObject> Slots = new Dictionary<Vector2Int, GameObject>();
    public GameObject SlotPrefab;
    
    public void CreateSlots(InventoryConfig config) {
        this.Slots = new Dictionary<Vector2Int, GameObject>();
        
        for(int x = 0; x < config.SlotsX; x++) {
            for(int y = 0; y < config.SlotsY; y++) {
                var gridCoordinates = new Vector2Int(x, y);

                var slot = this.InstantiateSlot(gridCoordinates);
                var slotUIPosition = new Vector2(x * this.GridLayoutGroup.cellSize.x, y * this.GridLayoutGroup.cellSize.y);
                var rect = slot.GetComponent<RectTransform>();
                rect.anchoredPosition = slotUIPosition;
                
                this.Slots.Add(gridCoordinates, slot);
            }
        }

        this.UpdateUISize(config.SlotsX, config.SlotsY);
        this.UpdateUIPosition();
    }

    public void ResetSlots() {
        foreach(var slot in this.Slots.Values) {
            Destroy(slot);
        }
        this.Slots.Clear();
    }

    private GameObject InstantiateSlot(Vector2 gridCoordinates) {
        var slot = Instantiate(this.SlotPrefab, this.transform);
        var itemSlotUI = slot.GetComponent<ItemSlotUI>();
        itemSlotUI.Position = gridCoordinates;
        return slot;
    }

    public void UpdateUISize(int slotsX, int slotsY) {
        var newSize = new Vector2(slotsX * this.GridLayoutGroup.cellSize.x, slotsY * this.GridLayoutGroup.cellSize.x);
        this.RectTransform.sizeDelta = newSize;
    }
    public void UpdateUIPosition() {
        var pos = new Vector2(this.RectTransform.rect.width / 2, -(this.RectTransform.rect.height / 2));
        this.RectTransform.anchoredPosition = pos;
    }
}
