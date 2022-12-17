using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorModule : MonoBehaviour {
    public Inventory Inventory;
    public ItemConfig ItemConfig;

    public void Awake() {
        if(this.Inventory == null) {
            return;
        }
        this.Inventory.CreateItemSlots();
        this.Inventory.TryAddItem(Item.Create(ItemConfig), new Vector2(0, 0));
    }
}
