using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {
	public Item Item;
	public SpriteRenderer SpriteRenderer;
	public CircleCollider2D Collider;
	
	//@TODO: auslagern in GameObjectPool
	public static DropItem Create(GameObject prefab, Vector2 position, Item item) {
		var dropItemGameObject = GameObject.Instantiate(prefab, position, Quaternion.identity);
		var dropItem = dropItemGameObject.GetComponent<DropItem>();
		dropItem.Item = item;
		dropItem.UpdateVisuals();
		dropItem.UpdateCollider();
		return dropItem;
	}
	
	private void UpdateVisuals() {
		this.SpriteRenderer.sprite = this.Item.Config.SpriteUI;
	}

	private void UpdateCollider(){
		this.Collider.radius = 0.5f;
	}
}
