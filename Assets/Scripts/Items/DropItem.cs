using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DropItem : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer SpriteRenderer;
    [SerializeField]
    private CapsuleCollider2D CapsuleCollider2D;

    public InventoryItem InventoryItem;

    private void Start() {
        this.SetSprite(this.InventoryItem.Item.Config.Sprite);
    }

    private void SetSprite(Sprite sprite) {
        this.SpriteRenderer.sprite = sprite;
    }

    public void Destroy() {
        GameObject.Destroy(this.gameObject);
    }

    //@TODO: Instanz auf Objektpool nutzen
    public static DropItem Create(InventoryItem inventoryitem, Vector2 position, GameObject prefab) {
        var dropItemObject = GameObject.Instantiate(prefab, position, Quaternion.identity);
        dropItemObject.GetComponent<DropItem>().InventoryItem = inventoryitem;
        var dropItem = dropItemObject.GetComponent<DropItem>();
        return dropItem;
    }
}
