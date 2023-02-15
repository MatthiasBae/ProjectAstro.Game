using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//IN Abstract class with EquipmentSlotUI?
public class InventoryGridUI : ItemContainerUIBase {
    public override void PlaceItem(InventoryItemUI inventoryItemUI){
        throw new NotImplementedException();
    }

    public override void PlaceItem(InventoryItemUI inventoryItemUI, Vector2 slotPosition){
        var unselected = InventoryUIController.Instance.TryUnselectItem();
        if (!unselected){
            return;
        }

        var anchoredPosition = new Vector2{
            x = Mathf.Floor(slotPosition.x * this.SlotSize.x),
            y = Mathf.Ceil(slotPosition.y * this.SlotSize.x)
        };
        inventoryItemUI.transform.SetParent(this.RectTransform, false);
        inventoryItemUI.SetPosition(anchoredPosition);
        this.AddItem(inventoryItemUI, slotPosition);
    }

    public override void RemoveItem(InventoryItemUI inventoryItemUI){
        base.RemoveItem(inventoryItemUI);
    }
    
    public override void AddItem(InventoryItemUI inventoryItemUI, Vector2 slotPosition){
        var endPosition = new Vector2{
            x = slotPosition.x + inventoryItemUI.Item.Config.Slots.x,
            y = slotPosition.y - inventoryItemUI.Item.Config.Slots.y
        };
        //Debug.Log(endPosition);
        for (var x = slotPosition.x; x < endPosition.x; x++){
            for (var y = slotPosition.y; y > endPosition.y; y--){
                var pos = new Vector2(x, y);
                this.Slots[pos]= inventoryItemUI;
                //Debug.Log($"Adding to slot in UI:{pos}");
            }
        }
    }

    public override Vector2 CalculateItemSize(Vector2 itemSlotCount, Vector2 gridSlotSize, Vector2 equipmentSlotSize){
        throw new NotImplementedException();
    }

    public override Vector2 CalculateAnchoredItemPosition(Vector2 itemSize, Vector2 equipmentSlotSize){
        throw new NotImplementedException();
    }

    public override void OnPointerClick(PointerEventData eventData){
        var inventoryUIController = InventoryUIController.Instance;
        var clickedLocalPosition = UserInterfaceHelper.MousePositionToRectTransformPosition(MouseHelper.MousePosition ,this.RectTransform);
        var clickedSlotPosition = this.AnchoredPositionToSlotPosition(clickedLocalPosition, this.SlotSize);
        
        if (!inventoryUIController.HasItemSelected){
            var selectedItemUI = this.GetItem(clickedSlotPosition);
            this.OnItemSelected(selectedItemUI);
            return;
				
        }

        this.OnItemDropped(inventoryUIController.SelectedInventoryItemUI, clickedSlotPosition);
    }
}
