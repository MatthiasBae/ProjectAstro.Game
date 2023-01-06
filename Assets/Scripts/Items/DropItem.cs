using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer SpriteRenderer;
    [SerializeField]
    private CapsuleCollider2D CapsuleCollider2D;
    
    public Item Item;

    private void Awake() {
        this.SetSprite(this.Item.Config.Sprite);
    }

    public void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            Debug.Log("Item in Gedanken aufgehoben");
        }
    }
    
    private void SetSprite(Sprite sprite) {
        this.SpriteRenderer.sprite = sprite;
    }
}
