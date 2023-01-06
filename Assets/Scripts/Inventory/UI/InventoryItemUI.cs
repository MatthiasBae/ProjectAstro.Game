using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour {
    
    [SerializeField]
    private Image Image;

    [SerializeField]
    private TMP_Text QuantityText;

    [SerializeField]
    private RectTransform RectTransform;

    public InventoryItem InventoryItem;

    private void Start() {
        this.Image.sprite = this.InventoryItem.Item.Config.SpriteUI;
    }
    public void UpdateUI() {
        this.SetSprite(this.InventoryItem.Item.Config.SpriteUI);
        this.SetQuantity(this.InventoryItem.Quantity);
    }
    public void SetSprite(Sprite sprite) {
        this.Image.sprite = sprite;
    }
    public void SetQuantity(int quantity) {
        this.QuantityText.text = quantity.ToString("00");
    }
    public void SetPosition(Vector2 position) {
        this.RectTransform.anchoredPosition = position;
    }
    public void SetSize(Vector2 size) {
        this.RectTransform.sizeDelta= size;
    }
}
