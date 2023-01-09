using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    private Image Image;
    [SerializeField]
    private TMP_Text QuantityText;
    [SerializeField]
    private RectTransform RectTransform;

    public InventoryUI InventoryUI;
    public InventoryItem InventoryItem;
    public InventoryDragDropUIController DragDropController;
    
    public void UpdateUI(Vector2 size) {
        var sprite = this.InventoryItem.Item.Config.Sprite;
        var quantity = this.InventoryItem.Quantity;
        
        this.SetSprite(sprite);
        this.SetQuantity(quantity);
        this.SetSize(size);
    }
    public void SetSprite(Sprite sprite) {
        this.Image.sprite = sprite;
    }
    public void SetQuantity(int quantity) {
        this.QuantityText.text = quantity.ToString("00");
    }
    public void SetAnchoredPosition(Vector2 position) {
        this.RectTransform.anchoredPosition = position;
    }
    public void SetPosition(Vector2 position) {
        this.RectTransform.position = position;
    }
    public void SetLocalPosition(Vector2 position) {
        this.RectTransform.localPosition = position;
    }
    public void SetSize(Vector2 size) {
        this.RectTransform.sizeDelta= size;
    }

    public void EnableRaycastTarget(bool enable) {
        this.Image.raycastTarget = enable;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(this.DragDropController.SelectedUIItem != null) {
            return;
        }

        this.DragDropController.SelectItem(this);
    }
}
